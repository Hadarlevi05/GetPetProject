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

        public MailHandler(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;

        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {

            MailAddress to = new MailAddress(mailRequest.ToEmail);
            MailAddress from = new MailAddress(_mailSettings.Mail);

            MailMessage message = new MailMessage(from, to);
            message.Subject = mailRequest.Subject;
            message.Body = "Elizabeth, as requested, sending you the invoice for Harry and Meghan's wedding. Any questions? Let me know.;";

            // get attachment by URL
            //String filePath = "wedding_invoice.pdf";
            //Attachment data = new Attachment(filePath, MediaTypeNames.Application.Octet);
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
                            var attachment = new Attachment(ms, MediaTypeNames.Application.Octet);
                            message.Attachments.Add(attachment);

                        }
                        //builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            //builder.HtmlBody = mailRequest.Body;
            //email.Body = builder.ToMessageBody();

            SmtpClient client = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
            {
                Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
                EnableSsl = true
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




            //var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            //email.Subject = mailRequest.Subject;
            //var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            //builder.HtmlBody = mailRequest.Body;
            //email.Body = builder.ToMessageBody();
            //using var smtp = new SmtpClient();



            //smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);
        }
    }
}
