using Domain.Entity;

namespace Persistence.Repositories;

/// <summary>
/// Repository for performing DB operations with FilmEntity.
/// </summary>
public interface IFilmRepository : IBaseRepository<FilmEntity, Guid>;

public class FilmRepository : BaseRepository<FilmEntity, Guid>, IFilmRepository
{
    public FilmRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}