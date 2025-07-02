using BlogFlow.Core.Application.Interface.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;


namespace BlogFlow.Core.Infrastructure.Mail.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            Log.Logger.Information($"Sending email to: {to}");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            email.Body = bodyBuilder.ToMessageBody();

            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpServerPort = int.Parse(_configuration["EmailSettings:Port"]);
            var userName = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpServer, smtpServerPort, true);
            await smtp.AuthenticateAsync(userName, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return true;
        }
    }
}
