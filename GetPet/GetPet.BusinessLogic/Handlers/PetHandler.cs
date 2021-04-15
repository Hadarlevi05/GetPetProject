using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
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
                var petToInsert = _mapper.Map<Pet>(pet);

                await _petRepository.AddAsync(petToInsert);

                _unitOfWork.SaveChanges();
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

        public async Task<IList<PetDto>> MyPets(PetFilter filter)
        {
            return null;
        }
    }
}