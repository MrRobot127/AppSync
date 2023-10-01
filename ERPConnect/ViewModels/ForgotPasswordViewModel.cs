using System.ComponentModel.DataAnnotations;

namespace ERPConnect.Web.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
