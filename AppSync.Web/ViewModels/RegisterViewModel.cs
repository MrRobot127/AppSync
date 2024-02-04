using AppSync.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppSync.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Remote(action: "IsUserNameAlreadyExist", controller: "Account")]
        public string UserName { get; set; }

        public int? PhoneNumber { get; set; }

        //[Required]
        //[EmailAddress]
        //[Remote(action: "IsEmailInUse", controller: "Account")]
        //[ValidEmailDomain(allowedDomain: "secureapp.com", ErrorMessage = "Email domain must be secureapp.com")]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
