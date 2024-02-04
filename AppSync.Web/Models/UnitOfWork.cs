using AppSync.Web.Interfaces;

namespace AppSync.Web.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
                            IMenuServiceRepository menuService,
                            IMasterEntryRepository master,
                            IOTPVerificationRepository otpVerification,
                            IEmailService emailService
                        )
        {
            MenuService = menuService;
            MasterEntry = master;
            OTPVerification = otpVerification;
            EmailService = emailService;
        }

        public IMenuServiceRepository MenuService { get; set; }
        public IMasterEntryRepository MasterEntry { get; set; }
        public IOTPVerificationRepository OTPVerification { get; set; }
        public IEmailService EmailService { get; set; }
    }
}
