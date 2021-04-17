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
    public interface IAnimalTraitRepository : IBaseRepository<AnimalTrait>
    {
        Task<IEnumerable<AnimalTrait>> SearchAsync(AnimalTraitFilter filter);
    }

    public class AnimalTraitRepository : BaseRepository<AnimalTrait>, IAnimalTraitRepository
    {
        public AnimalTraitRepository(
        GetPetDbContext getPetDbContext,
        IMapper mapper) :
        base(getPetDbContext)
        { }

        public override IQueryable<AnimalTrait> LoadNavigationProperties(IQueryable<AnimalTrait> query)
        {
            return query
                .Include(a => a.AnimalType)
                .Include(a => a.Trait);
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<AnimalTrait>> SearchAsync(AnimalTraitFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            if (filter.AnimalTypeId.HasValue)
            {
                query = query.Where(t => t.AnimalTypeId == filter.AnimalTypeId);
            }

            if (filter.TraitId.HasValue)
            {
                query = query.Where(t => t.TraitId == filter.TraitId);
            }

            return await query.ToListAsync();
        }

        public new async Task<AnimalTrait> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(AnimalTrait obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(AnimalTrait obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
