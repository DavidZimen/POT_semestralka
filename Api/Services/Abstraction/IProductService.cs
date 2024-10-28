using Domain.Dto;

namespace Api.Services.Abstraction;

public interface IProductService : IService
{
    Task<Product> GetProductByIdAsync(Guid productId);
    
    Task<Product> GetProductByNameAsync(string productName);
    
    Task<ICollection<Product>> GetAllProductsAsync();
    
    Task<Guid> CreateProductAsync(Product product);
    
    Task<bool> DeleteProductAsync(Guid productId);
}