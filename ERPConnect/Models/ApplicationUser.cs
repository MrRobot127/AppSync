using Microsoft.AspNetCore.Identity;

namespace ERPConnect.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ExternalEmail { get; set; }
    }
}
