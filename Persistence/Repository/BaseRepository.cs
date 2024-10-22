using Domain.Entity.Abstraction;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository.Abstractions;

namespace Persistence.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext DbContext;
    
    public BaseRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> FindByIdAsync(Guid entityId)
    {
        return await DbContext.Set<TEntity>().FindAsync(entityId);
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entityRes = (await DbContext.Set<TEntity>().AddAsync(entity)).Entity;
        await DbContext.SaveChangesAsync();
        return entityRes;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
        return (await DbContext.SaveChangesAsync()) > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
        return (await DbContext.SaveChangesAsync()) > 0;
    }
}