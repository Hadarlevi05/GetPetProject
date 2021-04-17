using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public AnimalTypesController(
            ILogger<AnimalTypesController> logger,
            IMapper mapper,
            IAnimalTypeRepository animalTypeRepository,
            IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _mapper = mapper;
            _animalTypeRepository = animalTypeRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AnimalTypeFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _animalTypeRepository.SearchAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var animalType = await _animalTypeRepository.GetByIdAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }

            return Ok(animalType);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AnimalType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var typeToInsert = _mapper.Map<City>(city);
            await _animalTypeRepository.AddAsync(type);
            await _unitOfWork.SaveChangesAsync();

            return Ok(type);
        }
    }
}
