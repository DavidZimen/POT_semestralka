using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class SeasonProfile : Profile
{
    public SeasonProfile()
    {
        CreateMap<SeasonEntity, SeasonDto>()
            .ForMember(dest => dest.SeasonId, opt => opt.MapFrom(src => src.Id));

        CreateMap<SeasonCreate, SeasonEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.ShowId));
        
        CreateMap<SeasonUpdate, SeasonEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ShowId, opt => opt.Ignore())
            .ForMember(dest => dest.Show, opt => opt.Ignore());
    }
}