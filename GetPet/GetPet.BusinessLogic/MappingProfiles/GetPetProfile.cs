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

            CityMapping();

            AnimalTypeMapping();

            TraitMapping();

            OrganizationMapping();

            PetTraitMapping();

            AnimalTraitMapping();

            TraitOptionsMapping();
        }

        private void TraitOptionsMapping()
        {
            CreateMap<TraitOption, TraitOptionDto>()
                .ReverseMap();
        }

        private void PetTraitMapping()
        {
            CreateMap<PetTrait, PetTraitDto>()
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet.Name))
                .ForMember(dest => dest.TraitName, opt => opt.MapFrom(src => src.Trait.Name))
                .ForMember(dest => dest.TraitValue, opt => opt.MapFrom(src => src.Description));

            CreateMap<PetTraitDto, PetTrait>()
                .ForMember(dest => dest.PetId, opt => opt.MapFrom(src => src.PetName))
                .ForMember(dest => dest.TraitId, opt => opt.MapFrom(src => src.TraitName)); 
                //.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Description));
        }

        private void UserMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));

            CreateMap<UserDto, User>()
                .ForPath(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));
        }

        private void PetMapping()
        {
            CreateMap<Pet, PetDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (src.Gender)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.MetaFileLinks.Select(mfl => mfl.Path)))
                .ForMember(dest => dest.Traits, opt => opt.MapFrom(src => src.PetTraits.ToDictionary(t => t.Trait.Name, t => t.Description)))
                .ForMember(dest => dest.AnimalTypeId, opt => opt.MapFrom(src => src.AnimalType.Id))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.DateHumanize()));

            CreateMap<PetDto, Pet>();
        }

        private void CityMapping()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }

        private void AnimalTypeMapping()
        {
            CreateMap<Data.Entities.AnimalType, AnimalTypeDto>();
        }

        private void TraitMapping()
        {
            CreateMap<Trait, TraitDto>();
        }

        private void OrganizationMapping()
        {
            CreateMap<Organization, OrganizationDto>().ReverseMap();
        }

        private void AnimalTraitMapping()
        {
            CreateMap<AnimalTrait, AnimalTraitDto>()
                .ForMember(dest => dest.TraitName, opt => opt.MapFrom(src => (src.Trait.Name)))
                .ForMember(dest => dest.AnimalTypeName, opt => opt.MapFrom(src => (src.AnimalType.Name)));

        }
    }
}