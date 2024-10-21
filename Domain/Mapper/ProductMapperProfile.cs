using AutoMapper;
using Domain.Dto;
using Domain.Entity;

namespace Domain.Mapper;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore());
        CreateMap<ProductEntity, Product>();
    }
}