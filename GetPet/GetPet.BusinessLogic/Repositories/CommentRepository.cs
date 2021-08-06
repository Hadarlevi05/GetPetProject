using AutoMapper;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{

    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> SearchAsync(CommentFilter filter);
    }

    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) : base(getPetDbContext)
        { }

        public override IQueryable<Comment> LoadNavigationProperties(IQueryable<Comment> query)
        {
            return query
                .Include(c => c.User);
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<Comment>> SearchAsync(CommentFilter filter)
        {
            var query = entities.AsQueryable();

            if (filter.ArticleId.HasValue)
            {
                query = query.Where(c => c.ArticleId == filter.ArticleId);
            }
            if (filter.UserId.HasValue)
            {
                query = query.Where(c => c.UserId == filter.UserId);
            }
            query = query.OrderBy(c => c.CreationTimestamp);
            query = base.SearchAsync(query, filter);

            return await query.ToListAsync();
        }

        public new async Task<Comment> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(Comment obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(Comment obj)
        {
            await base.UpdateAsync(obj);
        }
    }

}
