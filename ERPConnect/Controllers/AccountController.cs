using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models;
using ERPConnect.Web.Utility;
using ERPConnect.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using NuGet.Common;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ERPConnect.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        // ***************** Register Users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    PhoneNumber = Convert.ToString(model.PhoneNumber),
                    Email = null
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("FirstTimeLogin", "True"));

                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        // ***************** Login Users
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            if (TempData["RedirectFromFirstTimeLogin"] != null)
            {
                ViewBag.RedirectFromFirstTimeLogin = true;
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (!string.IsNullOrEmpty(model.Password) && (model.Email != null || model.UserName != null))
            {
                ApplicationUser user = null;

                if (model.LoginMethod == "username")
                {
                    user = await _userManager.FindByNameAsync(model.UserName);
                }
                else if (model.LoginMethod == "email")
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                }

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        // ***************** While User Registration Check if Entered User is Already in Use or not
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is aleady in use");
            }
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> IsUserNameAlreadyExist(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"UserName {UserName} is aleady in use");
            }
        }

        // ***************** Logout User and redirect to Login Page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var tokenEncoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                    // Build the password reset link                 

                    var callbackUrl = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = tokenEncoded }, Request.Scheme);


                    var emailSubject = "Reset Password";
                    var emailMessage = GeneratePasswordResetEmail(callbackUrl);

                    // Send the email
                    await _unitOfWork.EmailService.SendEmailAsync(model.Email, emailSubject, emailMessage);

                    // Send the user to Forgot Password Confirmation view
                    ViewBag.IsNeedToShowForgotPasswordConfirmation = true;
                    return View(model);
                }

                // Don't reveal that the user does not exist or is not confirmed
                ViewBag.IsNeedToShowForgotPasswordConfirmation = true;

                return View(model);
            }

            return View(model);
        }

        private string GeneratePasswordResetEmail(string resetLink)
        {
            // Read the content of the HTML file
            var emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "PasswordResetEmailTemplate.html");
            var emailTemplate = System.IO.File.ReadAllText(emailTemplatePath);

            // Replace {reset_link} with the actual reset link
            emailTemplate = emailTemplate.Replace("{reset_link}", resetLink);
            return emailTemplate;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // Decode the token from the URL
                    var tokenBytes = WebEncoders.Base64UrlDecode(model.Token);
                    var token = Encoding.UTF8.GetString(tokenBytes);

                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.IsNeedToShowPasswordReset = true;
                        return View(model);
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                //Dont reveal that the user does not exist
                ViewBag.IsNeedToShowPasswordReset = true;
                return View(model);
            }
            // Display validation errors if model state is not valid
            return View(model);
        }
    }
}
