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
using GetPet.BusinessLogic.Model.Filters;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraitOptionsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TraitOptionsController> _logger;
        private readonly ITraitOptionRepository _traitOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TraitOptionsController(
                ILogger<TraitOptionsController> logger,
                IMapper mapper,
                ITraitOptionRepository traitOptionRepository,
                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _traitOptionRepository = traitOptionRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] TraitOptionFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _traitOptionRepository.SearchAsync(filter));
        }
    }
}