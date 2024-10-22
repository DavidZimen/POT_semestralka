using Domain.Entity;
using Persistence.Repository.Abstractions;

namespace Persistence.Repository;

public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
{

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}