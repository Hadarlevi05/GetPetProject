using GetPet.Data.Entities;
using GetPet.Data.Enums;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers.Abstractions
{
    public interface IPetHandler
    {
        Task AddPet(Pet pet);

        Task SetPetStatus(int petId, PetStatus petStatus);
    }
}
