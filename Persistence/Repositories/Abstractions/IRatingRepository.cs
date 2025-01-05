using Domain.Dto;
using Domain.Entity;
using Domain.Search;

namespace Persistence.Repositories.Abstractions;

/// <summary>
/// Repository for processing rating in the application.
/// </summary>
public interface IRatingRepository
{
    /// <summary>
    /// Calculates the average rating with count based on provided search criteria.
    /// Only on of the parameters has to be not null.
    /// </summary>
    Task<(double? Average, int count)> GetAverageRating(
        Guid? filmId = null, Guid? showId = null, Guid? episodeId = null);
    
    /// <summary>
    /// Retrieves rating of the user to either film, show or episode of the show.
    /// </summary>
    /// <returns>Rating based on parameters or null, if it does not exist.</returns>
    Task<RatingEntity?> GetRatingOfUser(string userId, Guid? filmId = null, Guid? showId = null, Guid? episodeId = null);
}