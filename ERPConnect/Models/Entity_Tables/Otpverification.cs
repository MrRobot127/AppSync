namespace ERPConnect.Web.Models.Entity_Tables
{
    public partial class Otpverification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Otp { get; set; } = null!;
        public DateTime ExpirationTime { get; set; }
    }
}
