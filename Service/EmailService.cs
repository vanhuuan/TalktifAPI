using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using TalktifAPI.Dtos;
using TalktifAPI.Middleware;

namespace TalktifAPI.Service
{
    public class EmailService : IEmailService
    {
        private MailConfig mailSettings;

        public EmailService (IOptions<MailConfig> _mailSettings, ILogger<EmailService> _logger) {
            mailSettings = _mailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendMail(new MailContent() {
            To = email,
            Subject = subject,
            Body = htmlMessage
            });
        }

        public async Task SendMail(MailContent mailContent)
        {
            var email = new MimeMessage ();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add (MailboxAddress.Parse (mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody ();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try {
                smtp.Connect (mailSettings.Host, mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate (mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex) {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
                Console.WriteLine(ex);
            }
            smtp.Disconnect (true);
        }
    }
}