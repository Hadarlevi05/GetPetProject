using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using GetPet.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetAdoption.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoption.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PetsController> _logger;
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetsController(
            ILogger<PetsController> logger,
            IMapper mapper,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromBody]PetFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var pets = await _petRepository.SearchAsync(filter);

            return Ok(_mapper.Map<IEnumerable<PetDto>>(pets));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PetDto pet)
        {
            var petToInsert = _mapper.Map<Pet>(pet);

            await _petRepository.AddAsync(petToInsert);

            await _unitOfWork.SaveChangesAsync();

            return Ok(_mapper.Map<PetDto>(petToInsert));
        }
    }
}