using GetPet.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers
{
    public interface IMailHandler
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
