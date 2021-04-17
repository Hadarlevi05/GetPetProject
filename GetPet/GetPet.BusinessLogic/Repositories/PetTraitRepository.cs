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
    public interface IPetTraitRepository : IBaseRepository<PetTrait>
    {
        Task<IEnumerable<PetTrait>> SearchAsync(BaseFilter filter);

    }
    public class PetTraitRepository : BaseRepository<PetTrait>, IPetTraitRepository
    {
        public PetTraitRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public override IQueryable<PetTrait> LoadNavigationProperties(IQueryable<PetTrait> query)
        {
            return query
                .Include(u => u.Pet)
                .Include(u => u.Trait);
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<PetTrait>> SearchAsync(BaseFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            return await query.ToListAsync();
        }

        public new async Task<PetTrait> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(PetTrait obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(PetTrait obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
