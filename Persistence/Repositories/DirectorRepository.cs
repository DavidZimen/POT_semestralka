using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

/// <summary>
/// Repository for performing DB operations with DirectorEntity.
/// </summary>
public interface IDirectorRepository : IBaseRepository<DirectorEntity, Guid>
{
    /// <summary>
    /// Finds Director based on his personId foreign key, or returns null;
    /// </summary>
    /// <param name="personId">ID of person, that should be Director.</param>
    Task<DirectorEntity?> GetDirectorByPersonIdAsync(Guid personId);
}

public class DirectorRepository : BaseRepository<DirectorEntity, Guid>, IDirectorRepository
{
    public DirectorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<DirectorEntity?> GetDirectorByPersonIdAsync(Guid personId)
    {
        return DbSet.Where(director => director.PersonId == personId)
            .FirstOrDefaultAsync();
    }
}