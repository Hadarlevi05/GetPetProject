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
    public class OrganizationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationsController> _logger;
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationsController(
            ILogger<OrganizationsController> logger,
            IMapper mapper,
            IOrganizationRepository organizationRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _organizationRepository = organizationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<OrganizationDto>> Get([FromQuery] BaseFilter filter)
        {
            var organizations = await _organizationRepository.SearchAsync(filter);

            return _mapper.Map<IEnumerable<OrganizationDto>>(organizations);
        }
    }
}
