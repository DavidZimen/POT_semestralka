using Domain.Dto;

namespace Api.Services.Absttraction;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(Guid productId);
    
    Task<Product?> GetProductByNameAsync(string productName);
    
    Task<ICollection<Product>> GetAllProductsAsync();
    
    Task<Guid> CreateProductAsync(Product product);
    
    Task<bool> DeleteProductAsync(Guid productId);
}