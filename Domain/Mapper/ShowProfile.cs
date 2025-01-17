using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class ShowProfile : Profile
{
    public ShowProfile()
    {
        CreateMap<ShowEntity, ShowDto>()
            .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres));

        CreateMap<ShowCreate, ShowEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ShowCreate, ShowEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}