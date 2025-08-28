using AutoMapper;
using Church.Domain.Entities;
using Church.Domain.Models;

namespace Church.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Entity → ViewModel
            CreateMap<UserData, UserDataModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            // ViewModel → Entity
            CreateMap<UserDataModel, UserData>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                    Number = src.Number,
                    Complement = src.Complement,
                    City = src.City
                }));
        }
    }
}
