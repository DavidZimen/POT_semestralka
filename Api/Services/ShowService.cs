using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communication with repository related to Show data.
/// </summary>
public interface IShowService : IService
{
    /// <summary>
    /// Retrieves all non-deleted shows from DB.
    /// </summary>
    Task<ICollection<ShowDto>> GetAllShowsAsync();
    
    /// <summary>
    /// Retrieves show by its ID from database.
    /// </summary>
    /// <param name="showId">Show ID for the query.</param>
    /// <returns>Show with ID or null.</returns>
    Task<ShowDto?> GetShowByIdAsync(Guid showId);
    
    /// <summary>
    /// Creates a new Show in DB based on provided show parameter.
    /// </summary>
    /// <param name="showCreate">Object with requested show specification.</param>
    /// <returns>ID of newly created show entity.</returns>
    Task<ShowDto?> CreateShowAsync(ShowCreate showCreate);

    /// <summary>
    /// Updates show with ID to the new version.
    /// </summary>
    /// <param name="showUpdate"></param>
    /// <returns>New version of Show.</returns>
    Task<ShowDto?> UpdateShowAsync(ShowUpdate showUpdate);
    
    /// <summary>
    /// Deletes show with provided ID from DB.
    /// </summary>
    /// <param name="showId">ID of the show to be deleted.</param>
    /// <returns>True, if show was deleted.</returns>
    Task<bool> DeleteShowAsync(Guid showId);
    
    /// <summary>
    /// Retrieves all characters featured in show with provided ID.
    /// </summary>
    Task<ICollection<CharacterMediaDto>> GetShowCharactersAsync(Guid showId);
    
    /// <summary>
    /// Adds genre with genreId to show with showId.
    /// </summary>
    Task<bool> AddGenreAsync(Guid showId, Guid genreId);
    
    /// <summary>
    /// Removes genre with genreId from show with showId.
    /// </summary>
    Task<bool> RemoveGenreAsync(Guid showId, Guid genreId);
}

// TODO episode count to the show
public class ShowService : IShowService
{
    private readonly IShowRepository _showRepository;
    private readonly ICharacterService _characterService;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public ShowService(IShowRepository showRepository, IMapper mapper, IGenreRepository genreRepository, ICharacterService characterService)
    {
        _showRepository = showRepository;
        _genreRepository = genreRepository;
        _characterService = characterService;
        _mapper = mapper;
    }

    public async Task<ICollection<ShowDto>> GetAllShowsAsync()
    {
        return (await _showRepository.GetAllAsync())
            .Select(_mapper.Map<ShowDto>)
            .ToList();
    }

    public async Task<ShowDto?> GetShowByIdAsync(Guid showId)
    {
        var show = await _showRepository.FindByIdAsync(showId);
        return show is not null ? _mapper.Map<ShowDto>(show) : null;
    }

    public async Task<ShowDto?> CreateShowAsync(ShowCreate showCreate)
    {
        var showEntity = _mapper.Map<ShowEntity>(showCreate);
        
        showEntity = await _showRepository.CreateAsync(showEntity);
        return _mapper.Map<ShowDto>(showEntity);
    }

    public async Task<ShowDto?> UpdateShowAsync(ShowUpdate showUpdate)
    {
        var showEntity = await GetShowEntityOrThrow(showUpdate.ShowId);

        // map new values to entity
        _mapper.Map(showUpdate, showEntity);

        showEntity = await _showRepository.UpdateAsync(showEntity);
        return _mapper.Map<ShowDto>(showEntity);
    }

    public async Task<bool> DeleteShowAsync(Guid showId)
    {
        var showEntity = await GetShowEntityOrThrow(showId);
        return await _showRepository.DeleteAsync(showEntity);
    }

    public async Task<ICollection<CharacterMediaDto>> GetShowCharactersAsync(Guid showId)
    {
        var showEntity = await GetShowEntityOrThrow(showId);
        return await _characterService.GetCharactersForFilmOrShowAsync(showId: showEntity.Id);
    }

    public async Task<bool> AddGenreAsync(Guid showId, Guid genreId)
    {
        var showEntity = await GetShowEntityOrThrow(showId);
        var genreEntity = await _genreRepository.FindByIdAsync(genreId);
        if (genreEntity is null)
        {
            throw new BadRequestException($"Genre {genreId} not found");
        }
        showEntity.Genres.Add(genreEntity);
        showEntity = await _showRepository.UpdateAsync(showEntity);
        return showEntity is not null;
    }

    public async Task<bool> RemoveGenreAsync(Guid showId, Guid genreId)
    {
        var showEntity = await GetShowEntityOrThrow(showId);
        var removed = showEntity.Genres.RemoveAll(genre => genre.Id == genreId);
        if (removed == 0)
        {
            throw new BadRequestException($"Genre {genreId} not found");
        }
        
        showEntity = await _showRepository.UpdateAsync(showEntity);
        return showEntity is not null;
    }
    
    private async Task<ShowEntity> GetShowEntityOrThrow(Guid showId) {
        var showEntity = await _showRepository.FindByIdAsync(showId);
        if (showEntity is null)
        {
            throw new NotFoundException($"Show with id {showId} not found.");
        }

        return showEntity;
    }
}
