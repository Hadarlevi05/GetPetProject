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
    public class PetTraitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PetTraitsController> _logger;
        private readonly IPetTraitRepository _petTraitRepository;

        public PetTraitsController(
            ILogger<PetTraitsController> logger,
            IMapper mapper,
            IPetTraitRepository petTraitRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _petTraitRepository = petTraitRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PetTraitDto>> Get([FromQuery] BaseFilter filter)
        {
            var petTraits = await _petTraitRepository.SearchAsync(filter);

            return _mapper.Map<IEnumerable<PetTraitDto>>(petTraits);
        }
    }
}
