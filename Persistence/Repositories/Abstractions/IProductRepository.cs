using Domain.Entity;

namespace Persistence.Repositories.Abstractions;

public interface IProductRepository : IBaseRepository<ProductEntity, Guid>
{
    Task<ProductEntity?> FindProductByNameAsync(string name);
}