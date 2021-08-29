using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraitsController : BaseController
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

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TraitFilter filter)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var traits = await _traitRepository.SearchAsync(filter);

            return Ok(_mapper.Map<List<TraitDto>>(traits));
        }
    }
}