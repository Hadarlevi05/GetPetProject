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
    [Route("api/[controller]")]
    public class PetTraitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PetTraitsController> _logger;
        private readonly IPetTraitRepository _petTraitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetTraitsController(
            ILogger<PetTraitsController> logger,
            IMapper mapper,
            IPetTraitRepository petTraitRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _petTraitRepository = petTraitRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BaseFilter filter)
        {
            var petTraits = await _petTraitRepository.SearchAsync(filter);
            var mappedResults = _mapper.Map<IEnumerable<PetTraitDto>>(petTraits);

            return Ok(mappedResults);
        }

        //***it doesnt' work - should work with id's
        [HttpPost]
        public async Task<IActionResult> Post(PetTraitDto trait)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var petTraitToInsert = _mapper.Map<PetTrait>(trait);
            await _petTraitRepository.AddAsync(petTraitToInsert);
            await _unitOfWork.SaveChangesAsync();

            return Ok(petTraitToInsert);
        }
    }
}
