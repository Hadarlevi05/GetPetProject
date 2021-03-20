using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Data.Entities;

namespace GetPet.BusinessLogic.MappingProfiles
{
    public class GetPetProfile : Profile
    {
        public GetPetProfile()
        {
            CreateMap<Pet, PetDto>().ReverseMap();
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        }
    }
}
