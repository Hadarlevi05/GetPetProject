using GetPet.Data;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();        
        void SaveChanges();        
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

        public void SaveChanges()
        {
            _getPetDbContext.SaveChanges();
        }
    }
}