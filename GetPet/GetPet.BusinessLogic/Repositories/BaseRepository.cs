using GetPet.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoption.BusinessLogic.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private GetPetDbContext _context = null;
        protected DbSet<T> entities = null;

        public BaseRepository(GetPetDbContext getPetDbContext)
        {
            this._context = getPetDbContext;
            entities = _context.Set<T>();

        }
        public async Task DeleteAsync(object id)
        {
            T existing = await entities.FindAsync(id);
            entities.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await entities.FindAsync(id);
        }

        public async Task InsertAsync(T obj)
        {
            await entities.AddAsync(obj); ;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
