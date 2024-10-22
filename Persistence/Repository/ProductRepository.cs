using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository.Abstractions;

namespace Persistence.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductEntity?> FindProductByIdAsync(Guid productEntityId)
    {
        return await _dbContext.Set<ProductEntity>().FindAsync(productEntityId);
    }

    public async Task<ICollection<ProductEntity>> GetAllProductsAsync()
    {
        return await _dbContext.Set<ProductEntity>().ToListAsync();
    }

    public async Task<ProductEntity> CreateProductAsync(ProductEntity productEntity)
    {
        var product = (await _dbContext.Set<ProductEntity>().AddAsync(productEntity)).Entity;
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateProductAsync(ProductEntity productEntity)
    {
        var previousModifiedDate = productEntity.ModifiedDate;
        var newModifiedDate = _dbContext.Set<ProductEntity>().Update(productEntity).Entity.ModifiedDate;
        await _dbContext.SaveChangesAsync();
        return previousModifiedDate != newModifiedDate;
    }

    public async Task<bool> DeleteProductAsync(ProductEntity productEntity)
    {
        var result = _dbContext.Set<ProductEntity>().Remove(productEntity).Entity.DeletedDate is not null;
        await _dbContext.SaveChangesAsync();
        return result;
    }
}