using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> SearchAsync(BaseFilter filter);
    }
}
