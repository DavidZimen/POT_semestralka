using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

/// <summary>
/// Repository for processing rating in the application.
/// </summary>
public interface IRatingRepository : IBaseRepository<RatingEntity, Guid>
{
    /// <summary>
    /// Calculates the average rating with count based on provided search criteria.
    /// Only on of the parameters has to be not null.
    /// </summary>
    Task<(double? Average, int Count)> GetAverageRating(
        Guid? filmId = null, Guid? showId = null, Guid? episodeId = null);
    
    /// <summary>
    /// Retrieves rating of the user to either film, show or episode of the show.
    /// </summary>
    /// <returns>Rating based on parameters or null, if it does not exist.</returns>
    Task<RatingEntity?> GetRatingOfUser(string userId, Guid? filmId = null, Guid? showId = null, Guid? episodeId = null);
}

/// <summary>
/// Implementation of the <see cref="IRatingRepository"/> and extension of <see cref="BaseRepository{RatingEntity, Guid}"/>
/// </summary>
/// <param name="dbContext"></param>
public class RatingRepository(ApplicationDbContext dbContext)
    : BaseRepository<RatingEntity, Guid>(dbContext), IRatingRepository
{
    public async Task<(double? Average, int Count)> GetAverageRating(Guid? filmId = null, Guid? showId = null, Guid? episodeId = null)
    {
        var result = await FilterRatings(filmId, showId, episodeId)
            .GroupBy(_ => 1)
            .Select(g => new
            {
                Average = g.Average(r => r.Value),
                Count = g.Count()
            })
            .FirstOrDefaultAsync();

        return result != null ? (result.Average, result.Count) : (null, 0);
    }

    public async Task<RatingEntity?> GetRatingOfUser(string userId, Guid? filmId = null, Guid? showId = null, Guid? episodeId = null)
    {
        return await FilterRatings(filmId, showId, episodeId)
            .Where(r => r.UserId == userId)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Filters the values of rating in DB either for film, show or episode and return the Queryable for further processing.
    /// </summary>
    private IQueryable<RatingEntity> FilterRatings(Guid? filmId = null, Guid? showId = null, Guid? episodeId = null)
    {
        var query = DbSet.AsQueryable();

        // Apply the filtering logic based on the search criteria
        if (filmId.HasValue)
        {
            query = query.Where(r => r.FilmId == filmId.Value);
        }
        else if (showId.HasValue)
        {
            query = query.Where(r => r.ShowId == showId.Value);
        }
        else if (episodeId.HasValue)
        {
            query = query.Where(r => r.EpisodeId == episodeId.Value);
        }

        return query;
    } 
}