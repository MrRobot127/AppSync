using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Entity_Tables;
using static System.Net.WebRequestMethods;
using System.Security.Claims;
using ERPConnect.Web.Interfaces;

namespace ERPConnect.Web.Models.Repository
{
    public class OTPVerificationRepository : IOTPVerificationRepository
    {
        private readonly AppDbContext _dbContext;
        public OTPVerificationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SaveOTP(Otpverification otpVerification)
        {
            if (string.IsNullOrEmpty(otpVerification.Otp) || string.IsNullOrEmpty(otpVerification.UserId))
            {
                return false;
            }

            var existingOTPs = _dbContext.Otpverifications.Where(o => o.UserId == otpVerification.UserId).ToList();
            _dbContext.Otpverifications.RemoveRange(existingOTPs);

            _dbContext.Otpverifications.Add(otpVerification);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
