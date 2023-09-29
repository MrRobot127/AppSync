using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.Utility;
using ERPConnect.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;

namespace ERPConnect.Web.Controllers
{
    public class FirstTimeLoginController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly EmailService emailService;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;

        public FirstTimeLoginController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        EmailService emailService,
                                        IConfiguration configuration,
                                        IUnitOfWork unitOfWork
                                        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
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
                    ViewBag.UserExternalEmail = user.ExternalEmail ?? "";

                    if (string.IsNullOrEmpty(ViewBag.UserExternalEmail))
                    {
                        ViewBag.TempOtpSentStatus = TempData["TempOtpSentStatus"] as bool? ?? false;
                        ViewBag.TempExternalEmail = TempData["TempExternalEmail"] ?? "";
                    }
                }
            }

            if (TempData["Errors"] != null)
            {
                var errors = TempData["Errors"] as List<string>;
                ViewBag.Errors = errors ?? new List<string>();
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
                string Subject = "OTP Verification";
                string body = $"Your OTP is: {otp}";

                await emailService.SendEmailAsync(toEmail, Subject, body);

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
                        user.ExternalEmail = model.Email;
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
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            // Create a list to store errors
            var errors = new List<string>();

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
                    errors.Add("Old Pasword Not Valid.");
                }
            }
            else
            {
                errors.Add("User not found.");
            }

            // Store errors in TempData
            TempData["Errors"] = errors;

            return RedirectToAction("Index", "FirstTimeLogin");
        }


    }
}
