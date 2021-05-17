using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface ITraitRepository : IBaseRepository<Trait>
    {
        Task<IEnumerable<Trait>> SearchAsync(TraitFilter filter);
    }

    public class TraitRepository : BaseRepository<Trait>, ITraitRepository
    {
        public TraitRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public override IQueryable<Trait> LoadNavigationProperties(IQueryable<Trait> query)
        {
            return query
                .Include(t => t.TraitOptions);                
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<Trait>> SearchAsync(TraitFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            if (filter.AnimalTypeId.HasValue)
            {
                query = query.Where(t => t.AnimalTypeId == filter.AnimalTypeId);
            }

            return await query.ToListAsync();
        }

        public new async Task<Trait> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(Trait obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(Trait obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
