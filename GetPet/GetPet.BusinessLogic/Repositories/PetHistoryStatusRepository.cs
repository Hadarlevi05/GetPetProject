using AutoMapper;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IPetHistoryStatusRepository : IBaseRepository<PetHistoryStatus>
    {
        Task<IEnumerable<PetHistoryStatus>> SearchAsync(PetHistoryStatusFilter filter);
    }
    public class PetHistoryStatusRepository : BaseRepository<PetHistoryStatus>, IPetHistoryStatusRepository
    {
        public PetHistoryStatusRepository(
           GetPetDbContext getPetDbContext,
           IMapper mapper) :
           base(getPetDbContext)
        { }

        public override IQueryable<PetHistoryStatus> LoadNavigationProperties(IQueryable<PetHistoryStatus> query)
        {
            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<PetHistoryStatus>> SearchAsync(PetHistoryStatusFilter filter)
        {
            var query = entities.AsQueryable();

            if (filter.PetId.HasValue)
            {
                query = query.Where(phs => phs.PetId == filter.PetId);
            }
            query = base.SearchAsync(query, filter);

            return await query.ToListAsync();
        }

        public new async Task<PetHistoryStatus> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(PetHistoryStatus obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(PetHistoryStatus obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
