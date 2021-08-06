using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GetPet.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetPet.BusinessLogic.Model.Filters;

namespace GetPet.BusinessLogic.Repositories
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> SearchAsync(CityFilter filter);
    }

    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(
        GetPetDbContext getPetDbContext,
        IMapper mapper) :
        base(getPetDbContext)
        { }

        public override IQueryable<City> LoadNavigationProperties(IQueryable<City> query)
        {
            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<City>> SearchAsync(CityFilter filter)
        {
            var query = entities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(c => c.Name.StartsWith(filter.Name));
            }
            query = query.OrderBy(c => c.Name);
            query = base.SearchAsync(query, filter);


            return await query.ToListAsync();
        }

        public new async Task<City> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(City obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(City obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
