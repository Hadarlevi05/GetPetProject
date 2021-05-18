using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
            ILogger<UsersController> logger,
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _userRepository.SearchAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToInsert = _mapper.Map<User>(user);
            await _userRepository.AddAsync(userToInsert);
            await _unitOfWork.SaveChangesAsync();

            return Ok(userToInsert);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registeredUser = await _userRepository.Register(user);

            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var loginResponse = await _userRepository.Login(login);

                return Ok(new LoginResponseDto()
                {
                    Token = loginResponse.Token,
                    User = loginResponse.User
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
