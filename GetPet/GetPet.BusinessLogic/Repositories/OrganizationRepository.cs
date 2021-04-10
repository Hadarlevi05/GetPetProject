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
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Task<IEnumerable<Organization>> SearchAsync(BaseFilter filter);
    }
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public override IQueryable<Organization> LoadNavigationProperties(IQueryable<Organization> query)
        {
            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<Organization>> SearchAsync(BaseFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            return await query.ToListAsync();
        }

        public new async Task<Organization> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(Organization obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(Organization obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}