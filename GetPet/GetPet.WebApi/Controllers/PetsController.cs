using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.BusinessLogic.Repositories;
using GetPet.Common;
using GetPet.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : BaseController
    {
        private readonly IMetaFileLinkRepository _mflRepository;
        private readonly ITraitRepository _traitRepository;
        private readonly IPetHandler _petHandler;
        private readonly IMapper _mapper;
        private readonly ILogger<PetsController> _logger;
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetsController(
            IMetaFileLinkRepository mflRepository,
            ITraitRepository traitRepository,
            IPetHandler petHandler,
            ILogger<PetsController> logger,
            IMapper mapper,
            IPetRepository petRepository,
            IUnitOfWork unitOfWork)
        {
            _mflRepository = mflRepository;
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

        [HttpPost("search/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchCount([FromBody] PetFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var petCount = await _petRepository.SearchCountAsync(filter);

            return Ok(new CountResponseDto { Count = petCount });
        }

        private async Task<string> UploadFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var extension = formFile.FileName.Split(".").Last();
                var fileName = $"{Guid.NewGuid()}.{extension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload-content", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                return $"{Constants.WEBAPI_URL}/upload-content/{fileName}";
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PetDto pet)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
                };

                petToInsert.MetaFileLinks = new List<MetaFileLink>();
                foreach (var mflId in pet.MetaFileLinkIds)
                {
                    MetaFileLink mfl = _mflRepository.GetByIdAsync(mflId).Result;
                    petToInsert.MetaFileLinks.Add(mfl);
                }

                var traitsFilter = new TraitFilter
                {
                    AnimalTypeId = pet.AnimalTypeId
                };

                //get list of all traits by AnimalTypeId
                List<Trait> traitsByAnimal = _traitRepository.SearchAsync(traitsFilter).Result.ToList();

                petToInsert.PetTraits = new List<PetTrait>();
                foreach (KeyValuePair<string, string> entry in pet.Traits)
                {
                    //Use entry.Value & entry.Key
                    var foundTrait = traitsByAnimal.FirstOrDefault(traitItem => traitItem.Id == int.Parse(entry.Key));
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

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}