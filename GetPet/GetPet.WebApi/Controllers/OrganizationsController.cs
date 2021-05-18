using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GetPet.BusinessLogic;
using System;
using GetPet.Data.Entities;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationsController> _logger;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationsController(
            ILogger<OrganizationsController> logger,
            IMapper mapper,
            IOrganizationRepository organizationRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] OrganizationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _organizationRepository.SearchAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var org = await _organizationRepository.GetByIdAsync(id);
            if (org == null)
            {
                return NotFound();
            }

            return Ok(org);
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrganizationDto org)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orgToInsert = _mapper.Map<Organization>(org);
            await _organizationRepository.AddAsync(orgToInsert);
            await _unitOfWork.SaveChangesAsync();

            return Ok(orgToInsert);
        }
    }
}
