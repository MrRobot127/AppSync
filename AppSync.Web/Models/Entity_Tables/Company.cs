namespace AppSync.Web.Models.Entity_Tables
{
    public partial class Company
    {
        public int Id { get; set; }
        public int CompanyGroupId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? KeyPerson { get; set; }
        public string? InvolvingIndustry { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public string? Email { get; set; }
        public string? Pfno { get; set; }
        public string? Esino { get; set; }
        public string? HeadOffice { get; set; }
        public string? PanNo { get; set; }
        public string? RegNo { get; set; }
        public string? KeyPersonAddress { get; set; }
        public string? KeyPersonPhNo { get; set; }
        public string? KeyPersonDob { get; set; }
        public string? KeyDesignation { get; set; }
        public string? RegistrationDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
