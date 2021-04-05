using GetPet.Data;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly GetPetDbContext _getPetDbContext;

        public UnitOfWork(GetPetDbContext getPetDbContext)
        {
            _getPetDbContext = getPetDbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _getPetDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Added because SaveChangesAsync loses the exception info. 
        /// Need to investigate why
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() 
        {
            return _getPetDbContext.SaveChanges();
        }
    }
}