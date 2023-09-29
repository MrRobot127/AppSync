using ERPConnect.Web.Interfaces;

namespace ERPConnect.Web.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
                            IMenuServiceRepository menuService,
                            IMasterEntryRepository master,
                            IOTPVerificationRepository otpVerification

                        )
        {
            MenuService = menuService;
            MasterEntry = master;
            OTPVerification = otpVerification;
        }

        public IMenuServiceRepository MenuService { get; set; }
        public IMasterEntryRepository MasterEntry { get; set; }
        public IOTPVerificationRepository OTPVerification { get; set; }

    }
}
