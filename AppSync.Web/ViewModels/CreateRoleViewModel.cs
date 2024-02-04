using System.ComponentModel.DataAnnotations;

namespace AppSync.Web.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
