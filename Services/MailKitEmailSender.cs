using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MySite.Models;

namespace MySite.Services
{
    public class MailKitEmailSender(IOptions<SmtpSettings> smtpSettings) : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Vytvoření MIME zprávy
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_smtpSettings.Username, _smtpSettings.Username));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            // Použití MailKit's SmtpClient
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
