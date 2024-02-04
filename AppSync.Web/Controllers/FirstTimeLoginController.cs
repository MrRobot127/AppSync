using AppSync.Web.Interfaces;
using AppSync.Web.Models;
using AppSync.Web.Models.Entity_Tables;
using AppSync.Web.Utility;
using AppSync.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppSync.Web.Controllers
{
    public class FirstTimeLoginController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;

        public FirstTimeLoginController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        IConfiguration configuration,
                                        IUnitOfWork unitOfWork
                                        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    ViewBag.Email = user.Email ?? "";

                    if (string.IsNullOrEmpty(ViewBag.Email))
                    {
                        ViewBag.TempOtpSentStatus = TempData["TempOtpSentStatus"] as bool? ?? false;
                        ViewBag.TempExternalEmail = TempData["TempExternalEmail"] ?? "";
                    }
                }
            }

            if (TempData["Errors"] != null)
            {
                var errors = TempData["Errors"] as IEnumerable<string>;

                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmail(FirstTimeLoginViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                string secretKey = configuration["AppSettings:SecretKey"];

                string fromEmail = configuration["SmtpSettings:SmtpUsername"];

                string otp = OTPGenerator.GenerateOTP(secretKey);

                string toEmail = model.Email;
                string subject = "OTP Verification";
                string body = $"Dear User,\n\n" +
                              $"You have requested an OTP for verification. Please find your OTP below:\n\n" +
                              $"OTP: {otp}\n\n" +
                              $"Please use this OTP to verify your identity.\n\n" +
                              $"This OTP is valid for a limited time and should not be shared with anyone. If you did not request this OTP, please disregard this email.\n\n" +
                              $"Thank you for using our services.\n\n" +
                              $"Best regards,\n\n" +
                              $"Our Team";

                await unitOfWork.EmailService.SendEmailAsync(toEmail, subject, body);

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = userIdClaim?.Value ?? "";

                var otpVerification = new Otpverification
                {
                    UserId = userId,
                    FromEmail = fromEmail,
                    ToEmail = toEmail,
                    Otp = otp,
                    SentOn = DateTime.Now,
                    ExpirationTime = DateTime.Now.AddMinutes(5),
                    Type = "FirstTimeLogin"
                };

                bool status = await unitOfWork.OTPVerification.SaveOTP(otpVerification);

                if (status)
                {
                    TempData["TempOtpSentStatus"] = true;
                    TempData["TempExternalEmail"] = toEmail;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOTP(FirstTimeLoginViewModel model)
        {
            if (!string.IsNullOrEmpty(model.OTP) && !string.IsNullOrEmpty(model.Email))
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = userIdClaim?.Value ?? "";

                bool isOtpVerified = await unitOfWork.OTPVerification.VerifyOTP(model.OTP, userId);
                if (isOtpVerified)
                {
                    var user = await userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.EmailConfirmed = true;
                        var result = await userManager.UpdateAsync(user);

                        await unitOfWork.OTPVerification.DeleteOTP(model.OTP, userId);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(FirstTimeLoginViewModel model)
        {
            var errors = new List<string>();

            if (model.OldPassword == model.NewPassword)
            {
                errors.Add("Old and New Password Cannot be same.");
            }
            else
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {
                    var isOldPasswordValid = await userManager.CheckPasswordAsync(user, model.OldPassword);

                    if (isOldPasswordValid)
                    {
                        var changePasswordResult = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                        if (changePasswordResult.Succeeded)
                        {
                            var claims = await userManager.GetClaimsAsync(user);
                            var removeClaimResult = await userManager.RemoveClaimsAsync(user, claims);

                            if (removeClaimResult.Succeeded)
                            {
                                await signInManager.SignOutAsync();
                                TempData["RedirectFromFirstTimeLogin"] = true;
                                return RedirectToAction("Login", "Account");
                            }
                            else
                            {
                                errors.Add("Failed to remove the FirstLogin claim.");
                            }
                        }
                        else
                        {
                            foreach (var error in changePasswordResult.Errors)
                            {
                                errors.Add(error.Description);
                            }
                        }
                    }
                    else
                    {
                        errors.Add("Old Password Not Valid.");
                    }
                }
                else
                {
                    errors.Add("User not found.");
                }
            }

            if (errors.Count > 0)
            {
                TempData["Errors"] = errors;
            }

            return RedirectToAction("Index");
        }

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
    }
}
