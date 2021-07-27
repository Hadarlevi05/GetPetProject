using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GetPet.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Task<IEnumerable<Article>> SearchAsync(BaseFilter filter);
    }

    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(
        GetPetDbContext getPetDbContext,
        IMapper mapper) :
        base(getPetDbContext)
        { }

        public override IQueryable<Article> LoadNavigationProperties(IQueryable<Article> query)
        {
            return query
                .Include(a => a.User)
                .Include(a => a.MetaFileLink)
                .Include(a => a.Comments)
                    .ThenInclude(c => c.User);

        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<Article>> SearchAsync(BaseFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            query = query.OrderBy(c => c.CreationTimestamp);

            return await query.ToListAsync();
        }

        public new async Task<Article> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(Article obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(Article obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
