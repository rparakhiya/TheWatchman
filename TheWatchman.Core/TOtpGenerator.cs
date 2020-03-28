using System;
using System.Text;
using OtpNet;

namespace TheWatchman.Core
{
    public class TOtpGenerator
    {
        private readonly Totp totp;

        /// <summary>
        /// Token lifetime in seconds
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="tokenLifetime"></param>
        public TOtpGenerator(string secret, int tokenLifetime)
        {
            this.totp = new Totp(Encoding.ASCII.GetBytes(secret), tokenLifetime, OtpHashMode.Sha512);
        }

        public string GetOtp(DateTime? time = null)
        {
            return this.totp.ComputeTotp(time ?? DateTime.UtcNow);
        }
    }
}
