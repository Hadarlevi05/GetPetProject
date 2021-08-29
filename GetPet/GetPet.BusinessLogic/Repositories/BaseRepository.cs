using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private GetPetDbContext _context = null;

        protected DbSet<T> entities = null;

        public BaseRepository(
            GetPetDbContext getPetDbContext)
        {
            this._context = getPetDbContext;
            entities = _context.Set<T>();
        }

        public async Task DeleteAsync(int id)
        {
            T existing = await entities
                .SingleOrDefaultAsync(e => e.Id == id);

            if (existing == null)
                throw new Exception($"entity not exist with id: {id}");

            existing.IsDeleted = true;
        }

        public IQueryable<T> SearchAsync(IQueryable<T> query, BaseFilter filter)
        {
            query = LoadNavigationProperties(query);

            if (filter.Page > 1)
            {
                query = query
                    .Skip(filter.PerPage * (filter.Page - 1));
            }
            query = query
                .Take(filter.PerPage);

            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = entities.AsQueryable();

            query = LoadNavigationProperties(query);

            query = query
                .Where(e => e.Id == id);

            

            return await query.AsSplitQuery().SingleOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreationTimestamp =
                entity.UpdatedTimestamp =
                    DateTime.Now;

            await entities.AddAsync(entity);

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedTimestamp =
                DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public abstract IQueryable<T> LoadNavigationProperties(IQueryable<T> query);

        public async Task<IEnumerable<Trait>> SearchByQueryString(string stringquery)
        {
           return _context.Traits.FromSqlRaw(stringquery).ToList();
        }
    }
}