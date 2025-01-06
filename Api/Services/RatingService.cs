using System.Security.Claims;
using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories.Abstractions;

namespace Api.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RatingService(IRatingRepository ratingRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _ratingRepository = ratingRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ICollection<RatingDto>> GetRatingsAsync(Guid? filmId = null, Guid? showId = null, Guid? episodeId = null)
    {
        return (await _ratingRepository.GetAllAsync())
            .Select(_mapper.Map<RatingDto>)
            .ToList();
    }

    public async Task<AverageRatingDto> GetAverageRatingAsync(Guid? filmId = null, Guid? showId = null, Guid? episodeId = null)
    {
        var averageRating = await _ratingRepository.GetAverageRating(filmId, showId, episodeId);
        return new AverageRatingDto(averageRating.Average, averageRating.Count);
    }

    public async Task<RatingDto?> GetRatingAsync(Guid ratingId)
    {
        var rating = await _ratingRepository.FindByIdAsync(ratingId);
        return rating is not null ? _mapper.Map<RatingDto>(rating) : null;
    }

    public Task<Guid> CreateRatingAsync(CreateRatingRequest createRating)
    {
        var ratingEntity = _mapper.Map<RatingEntity>(createRating);
        ratingEntity.UserId = GetUserId() ?? throw new UnauthorizedAccessException();
        return _ratingRepository.CreateAsync(ratingEntity);
    }

    public async Task<RatingDto?> UpdateRatingAsync(UpdateRatingRequest updateRating)
    {
        var ratingEntity = await _ratingRepository.FindByIdAsync(updateRating.Id);

        // not found
        if (ratingEntity is null)
        {
            throw new NotFoundException($"Rating entity with id {updateRating.Id} does not exist.");
        }
        
        // owner or admin
        if (!ratingEntity.IsOwner(GetUserId()))
        {
            throw new ForbiddenException("You are not authorized to update this rating.");
        }
        
        ratingEntity.Value = updateRating.Value;
        ratingEntity.Description = updateRating.Description;
        
        return _mapper.Map<RatingDto>(await _ratingRepository.UpdateAsync(ratingEntity));
    }

    public async Task<bool> DeleteRatingAsync(Guid ratingId)
    {
        var ratingEntity = await _ratingRepository.FindByIdAsync(ratingId);

        // not found
        if (ratingEntity is null)
        {
            throw new NotFoundException($"Rating entity with id {ratingId} does not exist.");
        }
        
        // owner or admin
        if (!ratingEntity.IsOwner(GetUserId()))
        {
            throw new ForbiddenException("You are not authorized to delete this rating.");
        }
        
        return await _ratingRepository.DeleteAsync(ratingEntity);
    }

    public async Task<RatingDto?> GetUserRatingAsync(UserRatingRequest userRatingRequest)
    {
        var ratingOfUser = await _ratingRepository.GetRatingOfUser(userRatingRequest.UserId, userRatingRequest.FilmId,
            userRatingRequest.ShowId, userRatingRequest.EpisodeId);
        return ratingOfUser is not null ? _mapper.Map<RatingDto>(ratingOfUser) : null;
    }

    private string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    }
}