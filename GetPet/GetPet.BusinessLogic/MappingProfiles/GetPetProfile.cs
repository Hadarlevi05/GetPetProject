﻿using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Common;
using GetPet.Data.Entities;
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

            TraitOptionsMapping();

            ArticleMapping();

            CommentMapping();

            MetaFileLinkMapping();

            TraitOptionMapping();

            PetHistoryStatusMapping();
        }

        private void PetHistoryStatusMapping()
        {
            CreateMap<PetHistoryStatus, PetHistoryStatusDto>();
        }

        private void TraitOptionMapping()
        {
            CreateMap<TraitOption, TraitOptionDto>()
                .ReverseMap();
        }

        private void MetaFileLinkMapping()
        {
            CreateMap<MetaFileLink, MetaFileLinkDto>()
                .ReverseMap();
        }

        private void CommentMapping()
        {
            CreateMap<Comment, CommentDto>()
                .ReverseMap();
        }

        private void ArticleMapping()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.MetaFileLink == null ? null : src.MetaFileLink.Path))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.ToHtmlText()));


            CreateMap<ArticleDto, Article>();
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
                .ForMember(dest => dest.TraitValue, opt => opt.MapFrom(src => src.TraitOption != null ? src.TraitOption.Option : src.Description));

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
                .ForMember(dest => dest.Traits, opt => opt.MapFrom(src => src.PetTraits.Where(pt => !pt.Trait.IsBoolean).ToDictionary(t => src.Gender == Data.Enums.Gender.Male ? t.Trait.Name : t.Trait.FemaleName, t => t.TraitOption != null ? (src.Gender == Data.Enums.Gender.Male ? t.TraitOption.Option : t.TraitOption.FemaleOption) : t.Description)))
                .ForMember(dest => dest.BooleanTraits, opt => opt.MapFrom(src => src.PetTraits.Where(pt => pt.Trait.IsBoolean).ToDictionary(t => src.Gender == Data.Enums.Gender.Male ? t.Trait.Name : t.Trait.FemaleName, t => t.TraitOption != null ? (src.Gender == Data.Enums.Gender.Male ? t.TraitOption.Option : t.TraitOption.FemaleOption) : t.Description)))
                .ForMember(dest => dest.AnimalTypeId, opt => opt.MapFrom(src => src.AnimalType.Id))
                //.ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToLongDateString()));
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.HasValue ? src.Birthday.Value.ToLongDateString() : null));

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

    }
}