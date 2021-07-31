using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using GetPet.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GetPet.BusinessLogic.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using GetPet.BusinessLogic.Repositories;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : BaseController
    {
        private readonly ITraitRepository _traitRepository;
        private readonly IPetHandler _petHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<PetsController> _logger;
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetsController(
            ITraitRepository traitRepository,
            IPetHandler petHandler,
            ILogger<PetsController> logger,
            IMapper mapper,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork)
        {
            _traitRepository = traitRepository;
            _petHandler = petHandler;
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
            //var petToInsert = _mapper.Map<Pet>(pet);

            var petToInsert = new Pet
            {
                Name = pet.Name,
                Gender = pet.Gender,
                Birthday = pet.Birthday,
                Description = pet.Description,
                Source = pet.Source,
                SourceLink = pet.SourceLink,
                AnimalTypeId = pet.AnimalTypeId,
                UserId = pet.UserId
                //animal type?
            };

            petToInsert.MetaFileLinks = new List<MetaFileLink>();
            foreach (var imageSource in pet.Images)
            {
                petToInsert.MetaFileLinks.Add(
                    new MetaFileLink
                    {
                        Path = imageSource,
                        MimeType = imageSource.Substring(imageSource.LastIndexOf(".")),
                        Size = 1000
                    });
            }

            //get list of all traits
            var filter = new TraitFilter();
            var allTraits = _traitRepository.SearchAsync(filter).Result.ToList();

            //convert dictionary of <traitId, traitOptionid> to list of <PetTrait>
            List<Trait> allTraitsByAnimalType = allTraits.Where(x => x.AnimalTypeId == pet.AnimalTypeId).ToList();
            
            petToInsert.PetTraits = new List<PetTrait>();
            foreach (KeyValuePair<string, string> entry in pet.Traits)
            {
                //Use entry.Value & entry.Key
                var foundTrait = allTraitsByAnimalType.FirstOrDefault(traitItem => traitItem.Id == int.Parse(entry.Key));
                var foundTraitOption = foundTrait.TraitOptions.FirstOrDefault(op => op.Id == int.Parse(entry.Value));

                petToInsert.PetTraits.Add(
                    new PetTrait
                    {
                        Trait = foundTrait,
                        TraitOption = foundTraitOption
                    });
            }

            await _petHandler.AddPet(petToInsert);

            await _unitOfWork.SaveChangesAsync();

            return Ok(_mapper.Map<PetDto>(petToInsert));
            //return Ok(pet);
        }
    }
}