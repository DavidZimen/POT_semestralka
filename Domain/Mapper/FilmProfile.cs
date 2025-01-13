using System.Security.AccessControl;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<FilmEntity, FilmDto>()
            .ForMember(dest => dest.FilmId, opt => opt.MapFrom(src => src.Id));

        CreateMap<FilmCreate, FilmEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DirectorId, opt => opt.Ignore());
    }
}