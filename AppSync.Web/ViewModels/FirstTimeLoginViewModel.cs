using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppSync.Web.ViewModels
{
    public class FirstTimeLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "FirstTimeLogin")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "The OTP must not exceed 6 characters.")]
        public string? OTP { get; set; }

        [Required(ErrorMessage = "Old password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
