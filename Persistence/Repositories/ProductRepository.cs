using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class ProductRepository : BaseRepository<ProductEntity, Guid>, IProductRepository
{

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<ProductEntity?> FindProductByNameAsync(string name)
    {
        return DbSet.SingleOrDefaultAsync(productEntity => productEntity.Name == name);
    }
}