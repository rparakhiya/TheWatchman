using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit;

namespace TheWatchman.Server.Services.Email
{
    public interface IEmailServer
    {
        Task SendEmailAsync(MimeMessage message);
    }
}