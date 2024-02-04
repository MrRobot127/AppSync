using AppSync.Web.Models.Entity_Tables;
using AppSync.Web.ViewModels;

namespace AppSync.Web.Interfaces
{
    public interface IOTPVerificationRepository
    {
        Task<bool> SaveOTP(Otpverification otpVerification);

        Task<bool> VerifyOTP(string enteredOtp, string userId);

        Task<bool> DeleteOTP(string enteredOtp, string userId);

    }
}
