using GetPet.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.BusinessLogic.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        // Task<IEnumerable<T>> Search(PetFil   ter filter);
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
        Task SaveAsync();
    }
}
