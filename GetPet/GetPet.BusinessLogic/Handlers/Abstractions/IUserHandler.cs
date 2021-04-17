using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers.Abstractions
{
    public interface IUserHandler
    {
        Task<UserDto> Login(string email, string password);
        Task<UserDto> Register(UserDto user, Organization organization = null);
        Task SetEmailSubscription(UserDto userDto, bool subscribed);
        Task SetOrganization(UserDto user, Organization organization);
    }
}