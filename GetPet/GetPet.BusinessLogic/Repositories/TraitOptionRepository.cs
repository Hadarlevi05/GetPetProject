using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GetPet.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface ITraitOptionRepository : IBaseRepository<TraitOption>
    {
        Task<IEnumerable<TraitOption>> SearchAsync(TraitOptionFilter filter);
    }

    public class TraitOptionRepository : BaseRepository<TraitOption>, ITraitOptionRepository
    {
        public TraitOptionRepository(
        GetPetDbContext getPetDbContext,
        IMapper mapper) :
        base(getPetDbContext)
        { }

        public override IQueryable<TraitOption> LoadNavigationProperties(IQueryable<TraitOption> query)
        {
            //query = query
            //    .Include(q => q.Trait);

            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<TraitOption>> SearchAsync(TraitOptionFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            if(filter.TraitId.HasValue)
            {
                query = query.Where(o => o.TraitId == filter.TraitId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(o => o.Trait.Name.StartsWith(filter.Name));
            }

            return await query.ToListAsync();
        }

        public new async Task<TraitOption> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(TraitOption obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(TraitOption obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
