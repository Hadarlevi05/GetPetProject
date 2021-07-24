using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Handlers
{
    public class PetHandler : IPetHandler
    {
        protected readonly IPetRepository _petRepository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public PetHandler(IPetRepository petRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task SetPetStatus(int petId, PetStatus petStatus)
        {

        }

        public async Task AddPet(PetDto pet)
        {
            try
            {
                var images = pet.Images ?? new List<string>();

                var petToInsert = _mapper.Map<Pet>(pet);

                petToInsert.MetaFileLinks = new List<MetaFileLink>();
                foreach (var imageSource in images)
                {
                    petToInsert.MetaFileLinks.Add(
                        new MetaFileLink
                        {
                            Path = imageSource,
                            MimeType = imageSource.Substring(imageSource.LastIndexOf(".")),
                            Size = 1000
                        });
                }

                petToInsert.PetTraits = new List<PetTrait>();
                foreach (var trait in pet.TraitDTOs)
                {
                    petToInsert.PetTraits.Add(
                        new PetTrait()
                        {
                            Trait = trait.Key,
                            TraitOption = trait.Value,                           
                        }
                    );
                }               

                await _petRepository.AddAsync(petToInsert);
            }
            catch (Exception ex)
            {
                // TODO: Handle exception
            }
        }

        public async Task RemovePet(int petId, int userId)
        {

        }

        public async Task<IList<PetDto>> Search(PetFilter filter)
        {
            return null;
        }
    }
}