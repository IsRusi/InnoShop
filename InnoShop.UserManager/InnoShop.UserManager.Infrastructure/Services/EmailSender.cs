using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Infrastructure.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InnoShop.UserManager.Infrastructure.Services
{
    public class EmailSender(IOptions<SmtpSettings> smtpSettings, IOptions<EmailSettings> emailSettings) : IEmailService
    {
        public async Task SendConfirmationCodeAsync(string toEmail, string confirmationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings.Value.FromName, smtpSettings.Value.FromAddress));
            message.To.Add(new MailboxAddress("User", toEmail));
            message.Subject = "Confirm Your Email Address";
            
            var htmlBody = $@"
                <h2>Email Confirmation</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <p><a href='{confirmationLink}'>Confirm Email</a></p>
                <p>Or copy and paste this link in your browser:</p>
                <p>{confirmationLink}</p>
                <p>This link will expire in 30 minutes.</p>
                <p>If you did not create this account, please ignore this email.</p>
            ";

            message.Body = new TextPart("html") { Text = htmlBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.Value.Host, smtpSettings.Value.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpSettings.Value.FromAddress, smtpSettings.Value.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendPasswordResetAsync(string toEmail, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings.Value.FromName, smtpSettings.Value.FromAddress));
            message.To.Add(new MailboxAddress("User", toEmail));
            message.Subject = "Reset Your Password";
            
            var htmlBody = $@"
                <h2>Password Reset Request</h2>
                <p>We received a request to reset your password. Click the link below to proceed:</p>
                <p><a href='{resetLink}'>Reset Password</a></p>
                <p>Or copy and paste this link in your browser:</p>
                <p>{resetLink}</p>
                <p>This link will expire in 30 hour.</p>
                <p>If you did not request a password reset, please ignore this email.</p>
            ";

            message.Body = new TextPart("html") { Text = htmlBody };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.Value.Host, smtpSettings.Value.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpSettings.Value.FromAddress, smtpSettings.Value.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}