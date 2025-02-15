using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;
using Security.Enums;
using Security.Service;

namespace Api.Services;

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

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public RatingService(IRatingRepository ratingRepository, IMapper mapper, IAuthService authService, IUserRepository userRepository)
    {
        _ratingRepository = ratingRepository;
        _mapper = mapper;
        _authService = authService;
        _userRepository = userRepository;
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

    public async Task<Guid> CreateRatingAsync(RatingCreate ratingCreate)
    {
        var ratingEntity = _mapper.Map<RatingEntity>(ratingCreate);
        ratingEntity.UserId = _authService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();
        ratingEntity.User = (await _userRepository.FindByIdAsync(ratingEntity.UserId))!;
        return (await _ratingRepository.CreateAsync(ratingEntity)).Id;
    }

    public async Task<RatingDto?> UpdateRatingAsync(RatingUpdate ratingUpdate)
    {
        var ratingEntity = await _ratingRepository.FindByIdAsync(ratingUpdate.Id);

        // not found
        if (ratingEntity is null)
        {
            throw new NotFoundException($"Rating entity with id {ratingUpdate.Id} does not exist.");
        }
        
        // owner or admin
        if (!ratingEntity.IsOwner(_authService.GetCurrentUserId()) && !_authService.IsUserInRole(Role.Admin))
        {
            throw new ForbiddenException("You are not authorized to update this rating.");
        }
        
        ratingEntity.Value = ratingUpdate.Value;
        ratingEntity.Description = ratingUpdate.Description;
        
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
        if (!ratingEntity.IsOwner(_authService.GetCurrentUserId()) && !_authService.IsUserInRole(Role.Admin))
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
}