using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class EpisodeProfile : Profile
{
    public EpisodeProfile()
    {
        CreateMap<EpisodeEntity, EpisodeDto>()
            .ForMember(dest => dest.EpisodeId, opt => opt.MapFrom(src => src.Id));

        CreateMap<EpisodeCreate, EpisodeEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SeasonId, opt => opt.MapFrom(src => src.SeasonId));
        
        CreateMap<EpisodeUpdate, EpisodeEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SeasonId, opt => opt.Ignore())
            .ForMember(dest => dest.Season, opt => opt.Ignore());
    }
}