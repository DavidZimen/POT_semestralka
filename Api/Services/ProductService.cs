using Api.Services.Absttraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repository.Abstractions;

namespace Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        var productEntity = await _productRepository.FindByIdAsync(productId);
        return productEntity is null ? null : _mapper.Map<Product>(productEntity);
    }

    public async Task<Product?> GetProductByNameAsync(string productName)
    {
        var productEntity = await _productRepository.FindProductByNameAsync(productName);
        return productEntity is null ? null : _mapper.Map<Product>(productEntity);
    }

    public async Task<ICollection<Product>> GetAllProductsAsync()
    {
        return (await _productRepository.GetAllAsync())
            .Select(productEntity => _mapper.Map<Product>(productEntity))
            .ToList();
    }

    public Task<Guid> CreateProductAsync(Product product)
    {
        return _productRepository.CreateAsync(_mapper.Map<ProductEntity>(product));
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var productEntity = await _productRepository.FindByIdAsync(productId);
        if (productEntity is null)
            throw new NullReferenceException("Product not found");

        return await _productRepository.DeleteAsync(productEntity);
    }
}