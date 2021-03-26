using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoption.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PetsController> _logger;        
        private readonly IPetRepository _petRepository;

        public PetsController(
            ILogger<PetsController> logger,            
            IMapper mapper,
            IPetRepository petRepository)
        {
            _logger = logger;            
            _mapper = mapper;
            _petRepository = petRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PetDto>> Get()
        {
            var pets = await _petRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        [HttpPost]
        public async Task Post(PetDto pet)
        {
            var petToInsert = _mapper.Map<Pet>(pet);

            await _petRepository.InsertAsync(petToInsert);
            await _petRepository.SaveAsync();
        }
    }
}