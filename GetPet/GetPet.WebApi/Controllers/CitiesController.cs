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
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CitiesController(
                ILogger<CitiesController> logger,
                IMapper mapper,
                ICityRepository cityRepository,
                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CityFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _cityRepository.SearchAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = await _cityRepository.GetByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CityDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityToInsert = _mapper.Map<City>(city);
            await _cityRepository.AddAsync(cityToInsert);
            await _unitOfWork.SaveChangesAsync();

            return Ok(cityToInsert);
        }
    }
}