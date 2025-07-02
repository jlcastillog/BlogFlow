using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Transversal.Common.Contracts;
using MassTransit;
using Serilog;

namespace BlogFlow.Notifications.Worker.Consumer
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly IEmailService _emailService;

        public UserRegisteredConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            Log.Logger.Information($"Revived message {context.Message.messageID}");
            
            var subject = $"Welcome to BlogFlow! Your registration is complete 🎉\r\n";

            string emailBody = $@"
                <!DOCTYPE html>
                <html>
                <body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333;"">
                    <h2>Welcome to <span style=""color: #4CAF50;"">BlogFlow</span>!</h2>
                    <p>Hi {context.Message.Name},</p>
                    <p>We're excited to have you on board. Your account has been successfully created.</p>
                    <p>You can now log in and start using all the features available to you.</p>

                    <p><strong>Need help?</strong> Feel free to contact our support team anytime.</p>

                    <p>Thank you for joining us!<br/>
                    — The BlgFlow Team</p>

                    <hr />
                    <small>If you did not register for this account, please ignore this email.</small>
                </body>
                </html>";
            await _emailService.SendEmailAsync(context.Message.Email, subject, emailBody);
        }
    }
}
