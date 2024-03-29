﻿using GetPet.BusinessLogic.Model;
using System;
using System.Net;
using System.Net.Mail;
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
                    string imageName = attachment.Name;
                    attachment.ContentId = imageName;
                    message.Attachments.Add(attachment);                    
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