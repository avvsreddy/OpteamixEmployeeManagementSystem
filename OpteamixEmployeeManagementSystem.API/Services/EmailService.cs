using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using OpteamixEmployeeManagementSystem.Domain.Settings;
using MailKit.Net.Smtp;

namespace OpteamixEmployeeManagementSystem.API.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendPasswordResetEmailAsync(
            string toEmail,
            string resetToken)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.From));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "Password Reset Request";

            email.Body = new TextPart("html")
            {
                Text = $@"
    <p>Hi,</p>
    <p>We received a request to reset your password.</p>
    <p>Your reset token is:</p>
    <p><strong>{resetToken}</strong></p>
    <br/>
    <p>Use this token in reset-password API:</p>
    <p>Email: {toEmail}</p>
    <p>Token: {resetToken}</p>
    <p>New Password: your new password</p>
    <br/>
    <p>⚠️ This token expires in 1 hour.</p>
    <p>⚠️ If you didn't request this, ignore this email.</p>
    <br/>
    <p>Thanks,</p>
    <p>Opteamix Team</p>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _emailSettings.SmtpHost,
                _emailSettings.SmtpPort,
                SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(
                _emailSettings.Username,
                _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
