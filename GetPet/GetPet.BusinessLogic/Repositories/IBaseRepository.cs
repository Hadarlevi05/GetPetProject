using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoption.BusinessLogic.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> SearchAsync(IQueryable<T> query, BaseFilter filter);
        IQueryable<T> LoadNavigationProperties(IQueryable<T> query);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);        
    }
}