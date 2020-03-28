using System;
using System.Threading;
using System.Threading.Tasks;
using TheWatchman.Core;

namespace TheWatchman.OtpGenerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                var generator = new TOtpGenerator("sdf3223sdfwer23", 60);
                Console.WriteLine($"OTP: {generator.GetOtp()}");

                await Task.Delay(60000, CancellationToken.None);
            }
        }
    }
}
