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
    public class TraitsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TraitsController> _logger;
        private readonly ITraitRepository _traitRepository;

        public TraitsController(
            ILogger<TraitsController> logger,
            IMapper mapper,
            ITraitRepository traitRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _traitRepository = traitRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TraitDto>> Get([FromQuery] BaseFilter filter)
        {
            var traits = await _traitRepository.SearchAsync(filter);

            return _mapper.Map<IEnumerable<TraitDto>>(traits);
        }
    }
}
