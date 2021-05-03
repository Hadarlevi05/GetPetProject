using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            //if (user.PasswordHash != password)
            //    return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Register(UserDto userDto, Organization organization = null)
        {
            var user = _mapper.Map<User>(userDto);
            user.Organization = organization;
            await _userRepository.AddAsync(user);
            
            return userDto;
        }

        public async Task SetEmailSubscription(UserDto user, bool subscribed)
        {
            var entity = await _userRepository.GetByIdAsync(user.Id);
            entity.EmailSubscription = subscribed;
            await _userRepository.UpdateAsync(entity);
        }

        public async Task SetOrganization(UserDto user, Organization organization = null)
        {
            var entity = await _userRepository.GetByIdAsync(user.Id);
            entity.Organization = organization;
            await _userRepository.UpdateAsync(entity);
        }

    }
}