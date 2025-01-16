using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communicating with repository related to seasons.
/// </summary>
public interface ISeasonService : IService
{
    /// <param name="showId">ShowID for filtering seasons.</param>
    /// <returns>List of seasons belonging to the show.</returns>
    Task<ICollection<SeasonDto>> GetSeasonsAsync(Guid? showId);
    
    /// <summary>
    /// Retrieves season by its ID from database.
    /// </summary>
    /// <param name="seasonId">Season ID for the query.</param>
    /// <returns>Season with ID or null.</returns>
    Task<SeasonDto?> GetSeasonByIdAsync(Guid seasonId);

    /// <summary>
    /// Creates a new Season in DB based on provided season parameter.
    /// </summary>
    /// <param name="seasonCreate">Object with requested season specification.</param>
    /// <returns>ID of newly created season entity.</returns>
    Task<SeasonDto?> CreateSeasonAsync(SeasonCreate seasonCreate);

    /// <summary>
    /// Updates season with ID to the new version.
    /// </summary>
    /// <param name="seasonUpdate"></param>
    /// <returns>New version of Season.</returns>
    Task<SeasonDto?> UpdateSeasonAsync(SeasonUpdate seasonUpdate);

    /// <summary>
    /// Deletes season with provided ID from DB.
    /// </summary>
    /// <param name="seasonId">ID of the season to be deleted.</param>
    /// <returns>True, if season was deleted.</returns>
    Task<bool> DeleteSeasonAsync(Guid seasonId);
}

public class SeasonService : ISeasonService
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IMapper _mapper;

    public SeasonService(ISeasonRepository seasonRepository, IMapper mapper, IEpisodeRepository episodeRepository)
    {
        _seasonRepository = seasonRepository;
        _mapper = mapper;
        _episodeRepository = episodeRepository;
    }

    public async Task<SeasonDto?> GetSeasonByIdAsync(Guid seasonId)
    {
        var seasonEntity = await _seasonRepository.FindByIdAsync(seasonId);
        return await MapAndAddEpisodeCountAsync(seasonEntity);
    }

    public async Task<SeasonDto?> CreateSeasonAsync(SeasonCreate seasonCreate)
    {
        var seasonEntity = _mapper.Map<SeasonEntity>(seasonCreate);
        seasonEntity = await _seasonRepository.CreateAsync(seasonEntity);
        return await MapAndAddEpisodeCountAsync(seasonEntity);
    }

    public async Task<SeasonDto?> UpdateSeasonAsync(SeasonUpdate seasonUpdate)
    {
        var seasonEntity = await GetSeasonEntityOrThrow(seasonUpdate.SeasonId);

        // map new values to entity
        _mapper.Map(seasonUpdate, seasonEntity);

        seasonEntity = await _seasonRepository.UpdateAsync(seasonEntity);
        return await MapAndAddEpisodeCountAsync(seasonEntity);
    }

    public async Task<bool> DeleteSeasonAsync(Guid seasonId)
    {
        var seasonEntity = await GetSeasonEntityOrThrow(seasonId);
        return await _seasonRepository.DeleteAsync(seasonEntity);
    }

    public async Task<ICollection<SeasonDto>> GetSeasonsAsync(Guid? showId)
    {
        return (await _seasonRepository.GetSeasonsAsync(showId))
            .Select(_mapper.Map<SeasonDto>)
            .ToList();
    }
    
    private async Task<SeasonDto?> MapAndAddEpisodeCountAsync(SeasonEntity? seasonEntity)
    {
        if (seasonEntity is null)
        {
            return null;
        }
        
        var seasonDto = _mapper.Map<SeasonDto>(seasonEntity);
        seasonDto.EpisodeCount = await _episodeRepository.GetEpisodeCountBySeasonIdAsync(seasonEntity.Id);
        return seasonDto;
    }
    
    private async Task<SeasonEntity> GetSeasonEntityOrThrow(Guid seasonId) {
        var filmEntity = await _seasonRepository.FindByIdAsync(seasonId);
        if (filmEntity is null)
        {
            throw new NotFoundException($"Season with id {seasonId} not found.");
        }
        return filmEntity;
    }
}