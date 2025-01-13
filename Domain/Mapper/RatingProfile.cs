using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        // Map from RatingEntity to RatingDto
        CreateMap<RatingEntity, RatingDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        // Map from CreateRatingRequest to RatingEntity
        CreateMap<RatingCreate, RatingEntity>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.FilmId, opt => opt.MapFrom(src => src.FilmId))
            .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.ShowId))
            .ForMember(dest => dest.EpisodeId, opt => opt.MapFrom(src => src.EpisodeId))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore()); // Set explicitly in code
    }
}