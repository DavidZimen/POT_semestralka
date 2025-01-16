using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public interface IEpisodeRepository : IBaseRepository<EpisodeEntity, Guid>
{
    /// <summary>
    /// Number of episodes, that belongs to the show.
    /// </summary>
    /// <param name="showId">ID of the show</param>
    Task<int> GetEpisodeCountByShowIdAsync(Guid showId);
    
    /// <summary>
    /// Number of episodes, that belongs to the season.
    /// </summary>
    /// <param name="seasonId">ID of the season</param>
    Task<int> GetEpisodeCountBySeasonIdAsync(Guid seasonId);
}

public class EpisodeRepository : BaseRepository<EpisodeEntity, Guid>, IEpisodeRepository
{
    public EpisodeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<int> GetEpisodeCountByShowIdAsync(Guid showId)
    {
        return DbSet.CountAsync(episodeEntity => episodeEntity.Season.ShowId == showId);
    }

    public Task<int> GetEpisodeCountBySeasonIdAsync(Guid seasonId)
    {
        return DbSet.CountAsync(episodeEntity => episodeEntity.SeasonId == seasonId);
    }
}