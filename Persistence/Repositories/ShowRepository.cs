using Domain.Entity;

namespace Persistence.Repositories;

/// <summary>
/// Repository for manipulating DB data related to shows.
/// </summary>
public interface IShowRepository : IBaseRepository<ShowEntity, Guid>;


public class ShowRepository : BaseRepository<ShowEntity, Guid>, IShowRepository
{
    public ShowRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}