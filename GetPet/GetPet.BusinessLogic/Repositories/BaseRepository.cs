﻿using GetPet.BusinessLogic.Model;
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
                throw new System.Exception($"entity not exist with id: {id}");

            existing.IsDeleted = true;
        }

        public IQueryable<T> SearchAsync(IQueryable<T> query, BaseFilter filter)
        {
            query = LoadNavigationProperties(query);

            query = query
                .Skip(filter.PerPage * (filter.Page - 1))
                .Take(filter.PerPage);

            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = entities
                .Where(e => e.Id == id);

            query = LoadNavigationProperties(query);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreationTimestamp =
                entity.UpdatedTimestamp =
                    DateTime.UtcNow;

            await entities.AddAsync(entity);

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedTimestamp =
                DateTime.UtcNow;

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
