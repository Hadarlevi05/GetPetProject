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
        Task<IEnumerable<Pet>> SearchAsync(PetFilter filter);
    }
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(
            GetPetDbContext petContext,
            IMapper mapper) :
            base(petContext)
        { }

        public override IQueryable<Pet> LoadNavigationProperties(IQueryable<Pet> query)
        {
            query = query
                .Include(q => q.MetaFileLinks)
                .Include(q => q.AnimalType)
                .Include(q => q.PetTraits)
                    .ThenInclude(q => q.Trait)
                .Include(q => q.PetTraits)
                    .ThenInclude(q => q.TraitOption)
                .Include(q => q.User)
                    .ThenInclude(q => q.Organization)
                .Include(q => q.User)
                    .ThenInclude(q => q.City);

            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<Pet>> SearchAsync(PetFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            if (filter.CreatedSince.HasValue)
            {
                query = query.Where(p => p.CreationTimestamp > filter.CreatedSince.Value);
            }

            if (filter.AnimalTypes != null && filter.AnimalTypes.Any())
            {
                query = query.Where(p => filter.AnimalTypes.Contains(p.AnimalType.Id));
            }

            if (filter.TraitValues != null && filter.TraitValues.Any())
            {
                foreach (var traitValue in filter.TraitValues)
                {
                    if (traitValue.Value != null && traitValue.Value.Any())
                    {
                        query = query.Where(p =>
                            p.PetTraits.Any(pt =>
                                pt.TraitId == traitValue.Key &&
                                pt.TraitOptionId.HasValue && traitValue.Value.Contains(pt.TraitOptionId.Value)
                            )
                        );
                    }
                }
            }

            if (filter.BooleanTraits != null && filter.BooleanTraits.Any())
            {
                foreach (var boolTrait in filter.BooleanTraits)
                {
                    query = query.Where(p =>
                        p.PetTraits.Any(pt => pt.TraitId == boolTrait && pt.TraitOption.Option == "כן"));                    
                }
            }
            return await query.ToListAsync();
        }

        public new async Task<Pet> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }


        public new async Task UpdateAsync(Pet entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
