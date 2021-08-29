using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GetPet.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetPet.BusinessLogic.Model.Filters;

namespace GetPet.BusinessLogic.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<IEnumerable<Notification>> SearchAsync(NotificationFilter filter);
    }
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(
            GetPetDbContext petContext,
            IMapper mapper) :
            base(petContext)
        { }

        public override IQueryable<Notification> LoadNavigationProperties(IQueryable<Notification> query)
        {
            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<Notification>> SearchAsync(NotificationFilter filter)
        {
            var query = entities.AsQueryable();

            if (filter.UserId > 0)            
                query = query.Where(i => i.UserId == filter.UserId);

            query = base.SearchAsync(query, filter);

            return await query.ToListAsync();
        }

        public new async Task<Notification> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task UpdateAsync(Notification entity)
        {
            await base.UpdateAsync(entity);
        }
    }
}
