using ERPConnect.Web.Models.Entity_Tables;

namespace ERPConnect.Web.Interfaces
{
    public interface IOTPVerificationRepository
    {
        Task<bool> SaveOTP(Otpverification otpVerification);
    }
}
