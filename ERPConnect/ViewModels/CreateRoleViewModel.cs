using System.ComponentModel.DataAnnotations;

namespace ERPConnect.Web.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
