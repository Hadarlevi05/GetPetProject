using AutoMapper;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.Common;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IPetRepository : IBaseRepository<Pet>
    {
        Task<IEnumerable<Pet>> SearchAsync(PetFilter filter);
        string GetPetHashed(Pet pet);
        Task<bool> IsPetExist(Pet pet);
        Task<IEnumerable<Pet>> IsPetExist(IEnumerable<Pet> pets);
        Task<int> SearchCountAsync(PetFilter filter);
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
                .Include(q => q.PetHistoryStatuses)
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
            var query = entities.AsQueryable();

            query = GetSearchParameters(filter, query);

            query = base.SearchAsync(query, filter);

            return await query.AsSplitQuery().ToListAsync();
        }

        public async Task<int> SearchCountAsync(PetFilter filter)
        {
            var query = entities.AsQueryable();

            query = GetSearchParameters(filter, query);
            
            return await query.CountAsync();
        }

        private IQueryable<Pet> GetSearchParameters(PetFilter filter, IQueryable<Pet> query)
        {
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

            if (filter.PetStatus.HasValue)
            {
                query = query.Where(p => p.Status == filter.PetStatus.Value);
            }

            if (filter.PetSource.HasValue)
            {
                query = query.Where(p => p.Source == filter.PetSource.Value);
            }
            return query;
        }

        public new async Task<Pet> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task UpdateAsync(Pet entity)
        {
            await base.UpdateAsync(entity);
        }

        public string GetPetHashed(Pet pet)
        {
            var hashedExternalId = HashHelper.Sha256($"{pet.Name}_{pet.Description}_{pet.Source}");

            return hashedExternalId;
        }

        public async Task<bool> IsPetExist(Pet pet)
        {
            var hashedExternalId = GetPetHashed(pet);

            var exist = await entities
                .AnyAsync(p => p.ExternalId == hashedExternalId);

            return exist;
        }

        public async Task<IEnumerable<Pet>> IsPetExist(IEnumerable<Pet> pets)
        {
            var hashedExternalId = pets
                .Select(p => GetPetHashed(p));

            var petExists = await entities
                .Where(p => hashedExternalId.Contains(p.ExternalId))
                .ToListAsync();

            return petExists;
        }
    }
}
