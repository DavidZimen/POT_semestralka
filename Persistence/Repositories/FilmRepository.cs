using Domain.Entity;

namespace Persistence.Repositories;

/// <summary>
/// Repository for performing DB operations with FilmEntity.
/// </summary>
public interface IFilmRepository : IBaseRepository<FilmEntity, Guid>
{
    /// <param name="filmId">ID of the film.</param>
    /// <param name="actorId">ID of the actor.</param>
    /// <returns>True, if actor with given ID is already assigned to the film with given ID, otherwise false.</returns>
    bool HasFilmAssignedActor(Guid filmId, Guid actorId);
}

public class FilmRepository : BaseRepository<FilmEntity, Guid>, IFilmRepository
{
    public FilmRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public bool HasFilmAssignedActor(Guid filmId, Guid actorId)
    {
        return DbSet.Where(film => film.Id.Equals(filmId))
            .SelectMany(film => film.Actors)
            .Any(actor => actor.Id.Equals(actorId));
    }
}