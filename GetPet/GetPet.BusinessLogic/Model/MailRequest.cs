using System.Collections.Generic;
using System.Net.Mail;

namespace GetPet.BusinessLogic.Model
{
    public class MailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }

    }
}