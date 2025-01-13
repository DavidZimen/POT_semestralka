using Domain.Dto;

namespace Api.Services.Abstraction;

public interface IRatingService : IService
{
    Task<ICollection<RatingDto>> GetRatingsAsync(Guid? filmId = null, Guid? showId = null, Guid? episodeId = null);
    
    Task<AverageRatingDto> GetAverageRatingAsync(Guid? filmId = null, Guid? showId = null, Guid? episodeId = null);

    Task<RatingDto?> GetRatingAsync(Guid ratingId);
    
    Task<Guid> CreateRatingAsync(RatingCreate ratingCreate);
    
    Task<RatingDto?> UpdateRatingAsync(RatingUpdate ratingUpdate);
    
    Task<bool> DeleteRatingAsync(Guid ratingId);

    Task<RatingDto?> GetUserRatingAsync(UserRatingRequest userRatingRequest);
}