using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<CharacterEntity, CharacterActorDto>()
            .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => src.ShowId))
            .ForMember(dest => dest.FilmId, opt => opt.MapFrom(src => src.FilmId))
            .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.MediaName, opt => opt.MapFrom(src => 
                src.Film != null ? src.Film.Title : src.Show!.Title))
            .ForMember(dest => dest.PremiereDate, opt => opt.MapFrom(src => 
                src.Film != null ? src.Film.ReleaseDate : src.Show!.ReleaseDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => 
                src.Show != null ? src.Show.EndDate : null));
        
        CreateMap<CharacterEntity, CharacterMediaDto>()
            .ForMember(dest => dest.CharacterId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => 
                src.Film != null ? src.Film.Title : src.Show!.Title))
            .ForMember(dest => dest.Actor, opt => opt.MapFrom(src => src.Actor.Person));
        
        CreateMap<CharacterEntity, CharacterDto>()
            .ForMember(dest => dest.CharacterId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Name));
    }
}