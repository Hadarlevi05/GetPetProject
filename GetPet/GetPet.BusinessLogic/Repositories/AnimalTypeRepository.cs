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
    public interface IAnimalTypeRepository : IBaseRepository<AnimalType>
    {
        Task<IEnumerable<AnimalType>> SearchAsync(AnimalTypeFilter filter);
    }
    public class AnimalTypeRepository : BaseRepository<AnimalType>, IAnimalTypeRepository
    {
        public AnimalTypeRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public override IQueryable<AnimalType> LoadNavigationProperties(IQueryable<AnimalType> query)
        {
            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<AnimalType>> SearchAsync(AnimalTypeFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(c => c.Name.StartsWith(filter.Name));
            }

            return await query.ToListAsync();
        }

        public new async Task<AnimalType> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(AnimalType obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(AnimalType obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
