using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communicating with repository related to episodes.
/// </summary>
public interface IEpisodeService : IService
{
    /// <summary>
    /// Retrieves all episodes belonging to the season with seasonId.
    /// </summary>
    /// <param name="seasonId">SeasonId for filtering episodes.</param>
    /// <returns>List of seasons belonging to the show.</returns>
    Task<ICollection<EpisodeDto>> GetEpisodesAsync(Guid? seasonId = default);
    
    /// <summary>
    /// Finds the episode by its id.
    /// </summary>
    /// <param name="episodeId">ID of the episode to be found.</param>
    Task<EpisodeDto?> GetEpisodeByIdAsync(Guid episodeId);
    
    /// <summary>
    /// Creates a new Film in DB based on provided film parameter.
    /// </summary>
    /// <param name="episodeCreate">Object with requested episode specification.</param>
    /// <returns>ID of newly created film entity.</returns>
    Task<EpisodeDto?> CreateEpisodeAsync(EpisodeCreate episodeCreate);

    /// <summary>
    /// Updates episode with ID to the new version.
    /// </summary>
    /// <param name="episodeUpdate"></param>
    /// <returns>New version of Episode.</returns>
    Task<EpisodeDto?> UpdateEpisodeAsync(EpisodeUpdate episodeUpdate);
    
    /// <summary>
    /// Deleted film with provided ID from DB.
    /// </summary>
    /// <param name="episodeId">ID of the film to be deleted.</param>
    /// <returns>True, if episode was deleted.</returns>
    Task<bool> DeleteEpisodeAsync(Guid episodeId);
}

public class EpisodeService : IEpisodeService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IDirectorService _directorService;
    private readonly IMapper _mapper;

    public EpisodeService(IEpisodeRepository episodeRepository, IMapper mapper, IDirectorService directorService)
    {
        _episodeRepository = episodeRepository;
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<ICollection<EpisodeDto>> GetEpisodesAsync(Guid? seasonId = default)
    {
        return (await _episodeRepository.GetEpisodesAsync(seasonId))
            .Select(_mapper.Map<EpisodeDto>)
            .ToList();
    }

    public async Task<EpisodeDto?> GetEpisodeByIdAsync(Guid episodeId)
    {
        var episodeEntity = await _episodeRepository.FindByIdAsync(episodeId);
        return episodeEntity is not null ? _mapper.Map<EpisodeDto>(episodeEntity) : null;
    }

    public async Task<EpisodeDto?> CreateEpisodeAsync(EpisodeCreate episodeCreate)
    {
        var episodeEntity = _mapper.Map<EpisodeEntity>(episodeCreate);
        
        // find director and assign him to film
        var director = await _directorService.GetOrCreateDirectorAsync(episodeCreate.DirectorPersonId);
        episodeEntity.Director = director;
        
        episodeEntity = await _episodeRepository.CreateAsync(episodeEntity);
        return _mapper.Map<EpisodeDto>(episodeEntity);
    }

    public async Task<EpisodeDto?> UpdateEpisodeAsync(EpisodeUpdate episodeUpdate)
    {
        var episodeEntity = await GetEpisodeEntityOrThrowAsync(episodeUpdate.EpisodeId);

        // update director
        if (episodeEntity.Director.PersonId != episodeUpdate.DirectorPersonId)
        {
            var newDirector = await _directorService.GetOrCreateDirectorAsync(episodeUpdate.DirectorPersonId);
            episodeEntity.Director = newDirector;
        }

        // map new values to entity
        _mapper.Map(episodeUpdate, episodeEntity);

        episodeEntity = await _episodeRepository.UpdateAsync(episodeEntity);
        return _mapper.Map<EpisodeDto>(episodeEntity);
    }

    public async Task<bool> DeleteEpisodeAsync(Guid episodeId)
    {
        var episodeEntity = await GetEpisodeEntityOrThrowAsync(episodeId);
        return await _episodeRepository.DeleteAsync(episodeEntity);
    }
    
    private async Task<EpisodeEntity> GetEpisodeEntityOrThrowAsync(Guid episodeId) {
        var episodeEntity = await _episodeRepository.FindByIdAsync(episodeId);
        if (episodeEntity is null)
        {
            throw new NotFoundException($"Episode with id {episodeId} not found.");
        }
        return episodeEntity;
    }
}