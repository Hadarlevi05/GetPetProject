using AutoMapper;
using GetPet.BusinessLogic.Handlers.Abstractions;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
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
        protected readonly IPetHistoryStatusRepository _petHistoryStatusRepository;

        public PetHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPetRepository petRepository,
            IPetHistoryStatusRepository petHistoryStatusRepository)
        {
            _petRepository = petRepository;
            _petHistoryStatusRepository = petHistoryStatusRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task SetPetStatus(int petId, PetStatus petStatus)
        {
            await _petHistoryStatusRepository.AddAsync(new PetHistoryStatus
            {
                PetId = petId,
                Status = petStatus                                
            });
        }

        public async Task AddPet(Pet pet)
        {
            try
            {
                await _petRepository.AddAsync(pet);                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot insert pet {pet.Name}", ex);
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