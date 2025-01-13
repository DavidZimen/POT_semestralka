using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePerson, PersonEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Actor, opt => opt.Ignore())
            .ForMember(dest => dest.Director, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
        
        CreateMap<UpdatePerson, PersonEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Actor, opt => opt.Ignore())
            .ForMember(dest => dest.Director, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
        
        CreateMap<PersonEntity, PersonDto>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<PersonEntity, PersonMinimalDto>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id));
    }
}