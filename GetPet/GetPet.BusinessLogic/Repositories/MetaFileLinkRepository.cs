using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GetPet.BusinessLogic.Repositories
{
    public interface IMetaFileLinkRepository : IBaseRepository<MetaFileLink>
    {        

    }
    public class MetaFileLinkRepository : BaseRepository<MetaFileLink>, IMetaFileLinkRepository
    {
        public MetaFileLinkRepository(
            GetPetDbContext getPetDbContext,
            IMapper mapper) :
            base(getPetDbContext)
        { }

        public override IQueryable<MetaFileLink> LoadNavigationProperties(IQueryable<MetaFileLink> query)
        {
            return query;
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public new async Task<MetaFileLink> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task<MetaFileLink> AddAsync(MetaFileLink obj)
        {
            return await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(MetaFileLink obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}