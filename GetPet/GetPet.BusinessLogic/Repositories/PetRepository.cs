using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoption.BusinessLogic.Repositories
{
    public interface IPetRepository : IBaseRepository<Pet> 
    {
        Task<IEnumerable<Pet>> Search(PetFilter filter);
    }
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

        public async Task<IEnumerable<Pet>> Search(PetFilter filter)
        {
            var query = entities.AsQueryable();

            if (filter.CreatedSince.HasValue)
            {
                query = query.Where(p => p.CreationTimestamp > filter.CreatedSince.Value);
            }

            query
                .Skip(filter.PerPage * filter.Page - 1)
                .Take(filter.PerPage);

            return await query.ToListAsync();
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
