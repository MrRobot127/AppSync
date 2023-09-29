using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.Utility;
using ERPConnect.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
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
        public IActionResult Index()
        {
            var userExternalEmail = User.FindFirst("ExternalEmail")?.Value;

            ViewBag.UserExternalEmail = userExternalEmail ?? "";

            if (string.IsNullOrEmpty(ViewBag.UserExternalEmail))
            {
                ViewBag.TempOtpSentStatus = TempData["TempOtpSentStatus"] as bool? ?? false; 
                ViewBag.TempExternalEmail = TempData["TempExternalEmail"] ?? "";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmail(FirstTimeLoginViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                string secretKey = configuration["AppSettings:SecretKey"];
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
                    Otp = otp,
                    ExpirationTime = DateTime.Now.AddMinutes(5)
                };

                bool status = await unitOfWork.OTPVerification.SaveOTP(otpVerification);

                if (status)
                {
                    TempData["TempOtpSentStatus"] = true;
                    TempData["TempExternalEmail"] = toEmail;
                }
            }
            return RedirectToAction("FirstTimeLogin");
        }

        [HttpPost]
        public IActionResult VerifyOTP(FirstTimeLoginViewModel model)
        {           

            return RedirectToAction("FirstTimeLogin");
        }

    }
}
