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
    public class AnimalTraitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AnimalTraitsController> _logger;
        private readonly IAnimalTraitRepository _animalTraitRepository;

        public AnimalTraitsController(
            ILogger<AnimalTraitsController> logger,
            IMapper mapper,
            IAnimalTraitRepository animalTraitRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _animalTraitRepository = animalTraitRepository;
        }

        [HttpPost]
        public async Task<IEnumerable<AnimalTraitDto>> Get([FromBody] AnimalTraitFilter filter)
        {
            var types = await _animalTraitRepository.SearchAsync(filter);

            return _mapper.Map<IEnumerable<AnimalTraitDto>>(types);
        }
    }
}
