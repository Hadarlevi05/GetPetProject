using GetPet.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers.Abstractions
{
    public interface IPetHandler
    {
        Task AddPet(PetDto pet);
    }
}
