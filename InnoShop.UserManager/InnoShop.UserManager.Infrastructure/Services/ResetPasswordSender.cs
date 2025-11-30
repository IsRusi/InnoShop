using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InnoShop.UserManager.Infrastructure.Services
{
    internal class ResetPasswordSender(IOptions<SmtpSettings> smtpSettings, IOptions<EmailSettings> emailSettings) : IEmailService
    {
        public async Task SendConfirmationCodeAsync(string toEmail, string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings.Value.FromName, smtpSettings.Value.FromAddress));
            message.To.Add(new MailboxAddress(smtpSettings.Value.FromAddress, toEmail));
            message.Subject = $"Token for reset password {DateTime.UtcNow:yyyy/MM/dd/h}";
            message.Body = new TextPart() { Text = $@"this is your token reset password :{code}" };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.Value.Host, smtpSettings.Value.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpSettings.Value.FromAddress, smtpSettings.Value.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            ;

        }
    }
}
