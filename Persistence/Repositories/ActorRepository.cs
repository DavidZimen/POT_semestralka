using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

/// <summary>
/// Repository for performing DB operations with ActorEntity.
/// </summary>
public interface IActorRepository : IBaseRepository<ActorEntity, Guid>
{
    /// <summary>
    /// Finds Actor based on his personId foreign key, or returns null;
    /// </summary>
    /// <param name="personId">ID of person, that should be Actor.</param>
    Task<ActorEntity?> GetActorByPersonIdAsync(Guid personId);
}


public class ActorRepository : BaseRepository<ActorEntity, Guid>, IActorRepository
{
    public ActorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<ActorEntity?> GetActorByPersonIdAsync(Guid personId)
    {
        return DbSet.Where(actor => actor.PersonId == personId)
            .FirstOrDefaultAsync();
    }
}