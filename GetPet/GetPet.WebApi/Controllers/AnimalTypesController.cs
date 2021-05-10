using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AnimalTypesController> _logger;
        private readonly IAnimalTypeRepository _animalTypeRepository;

        public AnimalTypesController(
            ILogger<AnimalTypesController> logger,
            IMapper mapper,
            IAnimalTypeRepository animalTypeRepository)
        {
            _logger = logger;
           _mapper = mapper;
            _animalTypeRepository = animalTypeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AnimalTypeDto>> Get([FromQuery] BaseFilter filter)
        {
            var types = await _animalTypeRepository.SearchAsync(filter);

            return _mapper.Map<IEnumerable<AnimalTypeDto>>(types);
        }
    }
}
