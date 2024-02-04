using System.Security.Cryptography;

namespace AppSync.Web.Utility
{
    public class OTPGenerator
    {
        public static string GenerateOTP(string secretKey)
        {
            // Decode the secret key from Base32 to bytes
            byte[] keyBytes = Base32Decode(secretKey);

            // Determine the current time step (30-second interval)
            long timeStep = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;

            // Convert the time step to bytes (big-endian)
            byte[] counter = BitConverter.GetBytes(timeStep);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(counter);
            }

            // Create an HMAC-SHA1 instance using the key
            using (var hmac = new HMACSHA1(keyBytes))
            {
                // Compute the HMAC-SHA1 hash
                byte[] hash = hmac.ComputeHash(counter);

                // Get the offset (last 4 bits of the hash)
                int offset = hash[hash.Length - 1] & 0x0F;

                // Generate a 32-bit integer from the hash
                int binaryCode = BitConverter.ToInt32(hash, offset) & 0x7FFFFFFF;

                // Convert the integer to a 6-digit OTP
                string otp = (binaryCode % 1000000).ToString("D6");

                return otp;
            }
        }

        private static byte[] Base32Decode(string base32)
        {
            const string Base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

            base32 = base32.Trim().Replace("-", "").Replace(" ", "").ToUpper();
            byte[] buffer = new byte[base32.Length * 5 / 8];

            int bufferIndex = 0;
            int bitsLeft = 0;
            int bitsLeftCount = 0;

            foreach (char c in base32)
            {
                int value = Base32Chars.IndexOf(c);
                if (value < 0)
                {
                    throw new ArgumentException("Invalid Base32 character: " + c);
                }

                bitsLeft |= value << bitsLeftCount;
                bitsLeftCount += 5;

                if (bitsLeftCount >= 8)
                {
                    buffer[bufferIndex++] = (byte)(bitsLeft & 0xFF);
                    bitsLeft >>= 8;
                    bitsLeftCount -= 8;
                }
            }

            return buffer;
        }
    }
}
