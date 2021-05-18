using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TraitFilter filter)
        {

            var user = HttpContext.Items["User"];

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _traitRepository.SearchAsync(filter));
        }
    }
}