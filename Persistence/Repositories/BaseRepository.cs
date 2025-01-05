using Domain.Entity.Abstraction;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

/// <summary>
/// Implementation of the <see cref="IBaseRepository{TEntity,TKey}"/>
/// </summary>
public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    private readonly ApplicationDbContext _dbContext;
    
    protected readonly DbSet<TEntity> DbSet;
    
    protected BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = _dbContext.Set<TEntity>();
    }

    protected async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> FindByIdAsync(TKey entityId)
    {
        return await DbSet.FindAsync(entityId);
    }

    public async Task<int> CountAsync()
    {
        return await DbSet.CountAsync();
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<TKey> CreateAsync(TEntity entity)
    {
        var entityRes = await DbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entityRes.Entity.Id;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return await SaveChangesAsync() > 0;
    }
}