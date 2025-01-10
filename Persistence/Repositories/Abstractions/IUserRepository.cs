using Domain.Entity;

namespace Persistence.Repositories.Abstractions;

public interface IUserRepository : IRepository
{
    Task<UserEntity?> FindByIdAsync(string entityId);
    
    Task<UserEntity?> CreateAsync(UserEntity user);
    
    Task<bool> UpdateAsync(UserEntity user);
}