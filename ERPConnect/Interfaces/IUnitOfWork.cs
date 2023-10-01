namespace ERPConnect.Web.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmailService EmailService { get; set; }
        public IMenuServiceRepository MenuService { get; set; }
        public IMasterEntryRepository MasterEntry { get; set; }
        public IOTPVerificationRepository OTPVerification { get; set; }

    }
}
