using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Entity_Tables;
using static System.Net.WebRequestMethods;
using System.Security.Claims;
using ERPConnect.Web.Interfaces;
using ERPConnect.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> VerifyOTP(string enteredOtp, string userId)
        {
            if (!string.IsNullOrEmpty(enteredOtp) && !string.IsNullOrEmpty(userId))
            {
                string dbOtp = await GetOTPByUserId(userId);

                if (dbOtp.ToLower() == enteredOtp.ToLower())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<string> GetOTPByUserId(string userId)
        {
            var currentTime = DateTime.Now;

            var otpVerification = await _dbContext.Otpverifications
                .Where(o => o.UserId == userId && o.ExpirationTime >= currentTime)
                .OrderByDescending(o => o.ExpirationTime)
                .FirstOrDefaultAsync();

            return otpVerification.Otp ?? "";
        }

        public async Task<bool> DeleteOTP(string enteredOtp, string userId)
        {
            if (string.IsNullOrEmpty(enteredOtp) || string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var otpVerification = await _dbContext.Otpverifications
                .Where(o => o.UserId == userId && o.Otp == enteredOtp)
                .FirstOrDefaultAsync();

            if (otpVerification != null)
            {
                _dbContext.Otpverifications.Remove(otpVerification);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
