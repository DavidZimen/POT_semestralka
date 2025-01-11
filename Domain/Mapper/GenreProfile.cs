using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<GenreEntity, GenreDto>()
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}