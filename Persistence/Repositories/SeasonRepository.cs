using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

/// <summary>
/// Repository for manipulating DB data related to show seasons.
/// </summary>
public interface ISeasonRepository : IBaseRepository<SeasonEntity, Guid>
{
    /// <summary>
    /// Retrieves all seasons belonging to the show with showId.
    /// If showId is null, then all seasons of all shows.
    /// </summary>
    /// <param name="showId">ShowId for filtering.</param>
    Task<List<SeasonEntity>> GetSeasonsAsync(Guid? showId);
    
    /// <summary>
    /// Number of seasons, that belongs to the show.
    /// </summary>
    /// <param name="showId">ID of the show.</param>
    Task<int> GetSeasonCountByShowIdAsync(Guid showId);
}

public class SeasonRepository : BaseRepository<SeasonEntity, Guid>, ISeasonRepository
{
    public SeasonRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<List<SeasonEntity>> GetSeasonsAsync(Guid? showId)
    {
        var query = DbSet.AsQueryable();

        // Apply the filtering logic based on the search criteria
        if (showId.HasValue)
        {
            query = query.Where(r => r.ShowId == showId.Value);
        }
        
        return query.ToListAsync();
    }

    public Task<int> GetSeasonCountByShowIdAsync(Guid showId)
    {
        return DbSet.CountAsync(seasonEntity => seasonEntity.ShowId == showId);
    }
}