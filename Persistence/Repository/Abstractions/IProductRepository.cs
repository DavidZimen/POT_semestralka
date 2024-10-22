using Domain.Entity;

namespace Persistence.Repository.Abstractions;

public interface IProductRepository
{
    Task<ProductEntity?> FindProductByIdAsync(Guid productEntityId);

    Task<ICollection<ProductEntity>> GetAllProductsAsync();
    
    Task<ProductEntity> CreateProductAsync(ProductEntity productEntity);
    
    Task<bool> UpdateProductAsync(ProductEntity productEntity);
    
    Task<bool> DeleteProductAsync(ProductEntity productEntity);
}