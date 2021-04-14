using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Common;
using GetPet.Data.Entities;
using GetPet.Data.Enums;
using System;
using System.Linq;

namespace GetPet.BusinessLogic.MappingProfiles
{
    public class GetPetProfile : Profile
    {
        public GetPetProfile()
        {
            PetMapping();

            UserMapping();                
        }

        private void UserMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        }

        private void PetMapping()
        {
            CreateMap<Pet, PetDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (src.Gender)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.MetaFileLinks.Select(mfl => mfl.Path)))
                .ForMember(dest => dest.Traits, opt => opt.MapFrom(src => src.Traits.ToDictionary(t => t.Trait.Name, t => t.Value)))
                .ForMember(dest => dest.AnimalType, opt => opt.MapFrom(src => src.AnimalType.Name))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.DateHumanize()));

            CreateMap<PetDto, Pet>();
        }
    }
}