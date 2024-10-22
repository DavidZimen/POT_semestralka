using Domain.Entity.Abstraction;

namespace Persistence.Repository.Abstractions;

public interface IBaseRepository<TEntity> 
    where TEntity : BaseEntity
{
    Task<TEntity?> FindByIdAsync(Guid entityId);

    Task<ICollection<TEntity>> GetAllAsync();
    
    Task<TEntity> CreateAsync(TEntity entity);
    
    Task<bool> UpdateAsync(TEntity entity);
    
    Task<bool> DeleteAsync(TEntity entity);
}