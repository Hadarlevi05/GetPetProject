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