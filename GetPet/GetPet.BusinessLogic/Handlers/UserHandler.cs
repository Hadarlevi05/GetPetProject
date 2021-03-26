using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserDto> Login(string email, string password)
        {
            return null;
        }

        public Task<UserDto> Register(UserDto user, Organization organization = null)
        {
            return null;
        }

        public async Task SetEmailSubscription(bool subscribed)
        {

        }
    }
}