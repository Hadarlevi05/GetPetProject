using AutoMapper;
using GetPet.Data;
using GetPet.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoption.BusinessLogic.Repositories
{
    public interface IPetRepository : IBaseRepository<Pet> { }
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(
            GetPetDbContext petContext,
            IMapper mapper) :
            base(petContext)
        { }

        public new async Task DeleteAsync(object id)
        {
            await base.DeleteAsync(id);
        }

        public new async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public new async Task<Pet> GetByIdAsync(object id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task InsertAsync(Pet obj)
        {
            await base.InsertAsync(obj);
        }

        public new async Task SaveAsync()
        {
            await base.SaveAsync();
        }

        public new async Task UpdateAsync(Pet obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
