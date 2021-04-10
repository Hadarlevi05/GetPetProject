using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityRepository _cityRepository;


        public CitiesController(
                ILogger<CitiesController> logger,
                IMapper mapper,
                ICityRepository cityRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CityDto>> Get([FromQuery] BaseFilter filter)
        {
            var cities = await _cityRepository.SearchAsync(filter);

            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }
    }

}