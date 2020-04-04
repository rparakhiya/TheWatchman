using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace TheWatchman.Server.Services.Email
{
    public class EmailServer : IEmailServer
    {
        private readonly SmtpConfiguration _configuration;

        public EmailServer(SmtpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            using var smtpClient = new SmtpClient();
            smtpClient.Connect(_configuration.Server, _configuration.Port, _configuration.UseSsl);
            if (!String.IsNullOrEmpty(_configuration.Username))
            {
                smtpClient.Authenticate(_configuration.Username, _configuration.Password);
            }

            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
        }
    }
}