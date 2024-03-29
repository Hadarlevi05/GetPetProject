﻿using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GetPet.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetPet.BusinessLogic.Model.Filters;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IEmailHistoryRepository : IBaseRepository<EmailHistory>
    {
        Task<IEnumerable<EmailHistory>> SearchAsync(BaseFilter filter);

        Task<bool> EmailExists(int userId, int notificationId, DateTime sentDate);
    }

    public class EmailHistoryRepository : BaseRepository<EmailHistory>, IEmailHistoryRepository
    {
        public EmailHistoryRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        {

        }
        public override IQueryable<EmailHistory> LoadNavigationProperties(IQueryable<EmailHistory> query)
        {
            return query;
        }

        public async Task<IEnumerable<EmailHistory>> SearchAsync(BaseFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            //if (!string.IsNullOrWhiteSpace(filter.Name))
            //{
            //    query = query.Where(c => c.Name.StartsWith(filter.Name));
            //}

            //query = query.OrderBy(c => c.Name);

            return await query.ToListAsync();
        }

        public async Task<bool> EmailExists(int userId, int notificationId, DateTime sentDate)
        {
            return await entities.Where(eh => eh.UserId == userId && eh.NotificationId == notificationId && eh.SentDate == sentDate.Date)
                .AnyAsync();
        }

        public new async Task UpdateAsync(EmailHistory entity)
        {
            await base.UpdateAsync(entity);
        }

        async Task<IEnumerable<EmailHistory>> IEmailHistoryRepository.SearchAsync(BaseFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            query = query.OrderBy(c => c.CreationTimestamp);

            return await query.ToListAsync();
        }
    }

}
