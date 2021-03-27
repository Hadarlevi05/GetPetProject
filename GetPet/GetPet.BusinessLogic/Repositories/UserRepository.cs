using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<IEnumerable<User>> Search(BaseFilter filter);        
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public new async Task DeleteAsync(object id)
        {
            await base.DeleteAsync(id);
        }

        public new async Task<IEnumerable<User>> Search(BaseFilter filter)
        {
            return await entities
                .Include(u => u.City)
                .Include(u => u.Organization)
                .ToListAsync();
        }

        public new async Task<User> GetByIdAsync(object id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task InsertAsync(User obj)
        {
            await base.InsertAsync(obj);
        }

        public new async Task SaveAsync()
        {
            await base.SaveAsync();
        }

        public new async Task UpdateAsync(User obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
