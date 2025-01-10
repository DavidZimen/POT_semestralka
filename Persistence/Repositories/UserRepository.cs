using Domain.Entity;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserEntity?> FindByIdAsync(string entityId)
    {
        return await _dbContext.Set<UserEntity>().FindAsync(entityId);
    }

    public async Task<UserEntity?> CreateAsync(UserEntity user)
    {
        var userEntry = await _dbContext.Set<UserEntity>().AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return userEntry.Entity;
    }

    public async Task<bool> UpdateAsync(UserEntity user)
    {
        _dbContext.Set<UserEntity>().Update(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}