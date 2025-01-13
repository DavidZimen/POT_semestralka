using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class DirectorProfile : Profile
{
    public DirectorProfile()
    {
        CreateMap<FilmEntity, DirectorFilmDto>()
            .ForMember(dest => dest.FilmId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
        
       CreateMap<ShowEntity, DirectorShowDto>()
           .ForMember(dest => dest.ShowId, dest => dest.MapFrom(src => src.Id))
           .ForMember(dest  => dest.Title, opt => opt.MapFrom(src => src.Title))
           .ForMember(dest => dest.EpisodeCount, opt => opt.Ignore());
    }
}