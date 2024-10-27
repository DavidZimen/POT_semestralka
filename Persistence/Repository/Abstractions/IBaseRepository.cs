using Domain.Entity.Abstraction;

namespace Persistence.Repository.Abstractions;

public interface IBaseRepository<TEntity, TKey> 
    where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> FindByIdAsync(TKey entityId);

    Task<ICollection<TEntity>> GetAllAsync();
    
    Task<TKey> CreateAsync(TEntity entity);
    
    Task<bool> UpdateAsync(TEntity entity);
    
    Task<bool> DeleteAsync(TEntity entity);
}