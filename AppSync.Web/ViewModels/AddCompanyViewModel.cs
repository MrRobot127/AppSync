using System.ComponentModel.DataAnnotations;

namespace AppSync.Web.ViewModels
{
    public class AddCompanyViewModel
    {
        [Required]
        public int CompanyGroupId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? KeyPerson { get; set; }

        [Required]
        public string? InvolvingIndustry { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Pfno { get; set; }
        public string? Esino { get; set; }

        [Required]
        public string? HeadOffice { get; set; }
        public string? PanNo { get; set; }

        [Required]
        public string? RegNo { get; set; }
        public string? KeyPersonAddress { get; set; }
        public string? KeyPersonPhNo { get; set; }
        public string? KeyPersonDob { get; set; }
        public string? KeyDesignation { get; set; }

        [Required]
        public string? RegistrationDate { get; set; }

    }
}
