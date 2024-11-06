using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories.Abstractions;

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

    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        var productEntity = await _productRepository.FindByIdAsync(productId);
        
        if (productEntity is null)
            throw new ProductNotFoundException(productId);
        
        return _mapper.Map<Product>(productEntity);
    }

    public async Task<Product> GetProductByNameAsync(string productName)
    {
        var productEntity = await _productRepository.FindProductByNameAsync(productName);
        
        if (productEntity is null)
            throw new ProductNotFoundException(productName);
        
        return _mapper.Map<Product>(productEntity);
    }

    public async Task<ICollection<Product>> GetAllProductsAsync()
    {
        var products =  (await _productRepository.GetAllAsync())
            .Select(productEntity => _mapper.Map<Product>(productEntity))
            .ToList();

        if (products.Count == 0)
            throw new ProductsNotFoundException();

        return products;
    }

    public Task<Guid> CreateProductAsync(Product product)
    {
        return _productRepository.CreateAsync(_mapper.Map<ProductEntity>(product));
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var productEntity = await _productRepository.FindByIdAsync(productId);
        if (productEntity is null)
            throw new ProductNotFoundException(productId);

        return await _productRepository.DeleteAsync(productEntity);
    }
}