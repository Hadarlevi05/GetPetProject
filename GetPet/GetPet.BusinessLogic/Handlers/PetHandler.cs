using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Enums;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers
{
    public class PetHandler : IPetHandler
    {
        protected readonly IPetRepository _petRepository;

        public PetHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task SetPetStatus(int petId, PetStatus petStatus)
        { 
        
        }

        public async Task AddPet(PetDto pet)
        {

        }

        public async Task RemovePet(int petId, int userId)
        { 
        
        }

        public async Task<IList<PetDto>> Search(PetFilter filter)
        {
            return null;
        }

        public async Task<IList<PetDto>> MyPets(PetFilter filter)
        {
            return null;
        }
    }
}