using ERPConnect.Web.Models;
using ERPConnect.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERPConnect.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    await signInManager.SignInAsync(user, isPersistent: false);
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
        public IActionResult Login()
        {
            if (signInManager.IsSignedIn(User))
            {
                if (User.HasClaim("FirstTimeLogin", "True"))
                {
                    return RedirectToAction("FirstTimePasswordChange", "Account");
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        var hasFirstTimeLoginClaim = await userManager.GetClaimsAsync(user);

                        if (hasFirstTimeLoginClaim.Any(claim => claim.Type == "FirstTimeLogin" && claim.Value == "True"))
                        {
                            return RedirectToAction("FirstTimePasswordChange", "Account");
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "FirstTimePasswordChangePolicy")]
        public IActionResult FirstTimePasswordChange()
        {
            return View();
        }

        // ***************** While User Registration Check if Entered User is Already in Use or not
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }

            else
            {
                return Json($"Email {email} is aleady in use");
            }
        }


        // ***************** Logout User and redirect to Login Page
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
