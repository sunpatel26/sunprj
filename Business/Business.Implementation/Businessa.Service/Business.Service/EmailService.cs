using Business.Entities;
using Business.Entities.Setting;
using Business.Interface;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;

namespace Business.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSettingMetadata _mailSettings;
        public EmailService(IOptions<MailSettingMetadata> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public bool SendEmail(MailRequestMetadata mailRequest, bool isTemplateBody = false)
        {
            bool sendEmail = false;
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            string success = smtp.Send(email);
            smtp.Disconnect(true);
            if (!string.IsNullOrEmpty(success))
            {
                sendEmail = true;
            }
            return sendEmail;
        }

        public bool SendEmail(MailRequestMetadata mailRequest)
        {
            bool sendEmail = false;
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            if (mailRequest.Content != null)
            {
                builder.Attachments.Add("MeetingRequest.pdf", mailRequest.Content, ContentType.Parse("application/pdf"));
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            string success = smtp.Send(email);
            smtp.Disconnect(true);
            if (!string.IsNullOrEmpty(success))
            {
                sendEmail = true;
            }
            return sendEmail;
        }

        public bool SendEmail(MailRequest mailRequest)
        {
            throw new System.NotImplementedException();
        }

        void IEmailService.SendEmail(MailRequestMetadata request)
        {
            throw new System.NotImplementedException();
        }
    }
}