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
    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<IEnumerable<User>> SearchAsync(BaseFilter filter);        
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public override IQueryable<User> LoadNavigationProperties(IQueryable<User> query)
        {
            return query
                .Include(u => u.Organization)
                .Include(u => u.City);                
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> SearchAsync(BaseFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            return await query.ToListAsync();
        }

        public new async Task<User> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(User obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(User obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}