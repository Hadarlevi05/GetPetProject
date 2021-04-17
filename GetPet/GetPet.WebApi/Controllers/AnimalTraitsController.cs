using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetPet.Data.Entities;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalTraitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AnimalTraitsController> _logger;
        private readonly IAnimalTraitRepository _animalTraitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AnimalTraitsController(
                ILogger<AnimalTraitsController> logger,
                IMapper mapper,
                IAnimalTraitRepository animalTraitRepository,
                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _animalTraitRepository = animalTraitRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AnimalTraitFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var filteredRes = await _animalTraitRepository.SearchAsync(filter);
            var mappedRes = _mapper.Map<IEnumerable<AnimalTraitDto>>(filteredRes);
            return Ok(mappedRes);
        }
    }
}