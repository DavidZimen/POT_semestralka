using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<CharacterEntity, CharacterMediaDto>()
            .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MediaName, opt => opt.MapFrom(entity => 
                entity.Film != null ? entity.Film.Title : entity.Show!.Title))
            .ForMember(dest => dest.PremiereDate, opt => opt.MapFrom(entity => 
                entity.Film != null ? entity.Film.ReleaseDate : entity.Show!.ReleaseDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(entity => 
                entity.Show != null ? entity.Show.EndDate : null));
        
        CreateMap<CharacterEntity, CharacterDto>()
            .ForMember(dest => dest.CharacterId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Name));
    }
}