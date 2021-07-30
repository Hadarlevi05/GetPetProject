using GetPet.BusinessLogic.Model;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers
{
    public class MailHandler : IMailHandler
    {
        private readonly MailSettings _mailSettings;

        public MailHandler(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MailAddress to = new MailAddress(mailRequest.To);

            to = new MailAddress("hadarlevi05@gmail.com");

            MailAddress from = new MailAddress(_mailSettings.Mail);

            MailMessage message = new MailMessage(from, to);
            message.Subject = mailRequest.Subject;
            message.Body = mailRequest.Body;
            message.IsBodyHtml = true;

            if (mailRequest.Attachments != null)
            {
                foreach (var attachment in mailRequest.Attachments)
                {
                    attachment.ContentDisposition.Inline = true;
                    string imageName = $"{Guid.NewGuid()}.jpg";
                    attachment.ContentId = imageName;
                    message.Attachments.Add(attachment);

                    message.Body = message.Body.Replace("{{pet-image}}", $"cid:{imageName}");                    
                }
            }

            SmtpClient client = new(_mailSettings.Host, _mailSettings.Port)
            {
                Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
                EnableSsl = true,
                UseDefaultCredentials = false,                 
            };

            // code in brackets above needed if authentication required
            try
            {
                client.SendAsync(message, null);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


    }
}
