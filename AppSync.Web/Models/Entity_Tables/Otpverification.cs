namespace AppSync.Web.Models.Entity_Tables
{
    public partial class Otpverification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public string Otp { get; set; } = null!;
        public DateTime SentOn { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Type { get; set; } = null!;
    }
}
