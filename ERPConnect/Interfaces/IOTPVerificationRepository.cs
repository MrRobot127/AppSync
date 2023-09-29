using ERPConnect.Web.Models.Entity_Tables;
using ERPConnect.Web.ViewModels;

namespace ERPConnect.Web.Interfaces
{
    public interface IOTPVerificationRepository
    {
        Task<bool> SaveOTP(Otpverification otpVerification);

        Task<bool> VerifyOTP(string enteredOtp, string userId);

        Task<bool> DeleteOTP(string enteredOtp, string userId);

    }
}
