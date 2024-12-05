using Application.Helpers;
using Application.Services.Interfaces;
using Infraestructure.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Application.Services.Classes
{
    public class EmailService : IEmailService
    {
        private EmailSettings emailsettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailsettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest emailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailsettings.SenderName,
                                                    emailsettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = emailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(emailsettings.Server, emailsettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailsettings.SenderEmail, emailsettings.Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
