using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communication with repository related to Film data.
/// </summary>
public interface IFilmService : IService
{
    /// <summary>
    /// Retrieves all non deleted films from DB.
    /// </summary>
    Task<ICollection<FilmDto>> GetAllFilmsAsync();
    
    /// <summary>
    /// Retrieves film by its ID from database.
    /// </summary>
    /// <param name="filmId">Film ID for the query.</param>
    /// <returns>Film with ID or null.</returns>
    Task<FilmDto?> GetFilmByIdAsync(Guid filmId);
    
    /// <summary>
    /// Creates a new Film in DB based on provided film parameter.
    /// </summary>
    /// <param name="filmCreate">Object with requested film specification.</param>
    /// <returns>ID of newly created film entity.</returns>
    Task<FilmDto?> CreateFilmAsync(FilmCreate filmCreate);

    /// <summary>
    /// Updates film with ID to the new version.
    /// </summary>
    /// <param name="filmUpdate"></param>
    /// <returns>New version of Film.</returns>
    Task<FilmDto?> UpdateFilmAsync(FilmUpdate filmUpdate);
    
    /// <summary>
    /// Deleted film with provided ID from DB.
    /// </summary>
    /// <param name="filmId">ID of the film to be deleted.</param>
    /// <returns>True, if film was deleted.</returns>
    Task<bool> DeleteFilmAsync(Guid filmId);
    
    /// <summary>
    /// Retrieves all characters featured in film with provided ID.
    /// </summary>
    Task<ICollection<CharacterMediaDto>> GetFilmCharactersAsync(Guid filmId);
    
    /// <summary>
    /// Adds genre with genreId to film with filmId.
    /// </summary>
    Task<bool> AddGenreAsync(Guid filmId, Guid genreId);
    
    /// <summary>
    /// Removes genre with genreId from film with filmId.
    /// </summary>
    Task<bool> RemoveGenreAsync(Guid filmId, Guid genreId);
}

public class FilmService : IFilmService
{
    private readonly IFilmRepository _filmRepository;
    private readonly IDirectorService _directorService;
    private readonly ICharacterService _characterService;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public FilmService(IFilmRepository filmRepository, IDirectorService directorService, IActorService actorService, IMapper mapper, IGenreRepository genreRepository, ICharacterService characterService)
    {
        _filmRepository = filmRepository;
        _directorService = directorService;
        _genreRepository = genreRepository;
        _characterService = characterService;
        _mapper = mapper;
    }

    public async Task<ICollection<FilmDto>> GetAllFilmsAsync()
    {
        return (await _filmRepository.GetAllAsync())
            .Select(_mapper.Map<FilmDto>)
            .ToList();
    }

    public async Task<FilmDto?> GetFilmByIdAsync(Guid filmId)
    {
        var film = await _filmRepository.FindByIdAsync(filmId);
        return film is not null ? _mapper.Map<FilmDto>(film) : null;
    }

    public async Task<FilmDto?> CreateFilmAsync(FilmCreate filmCreate)
    {
        var filmEntity = _mapper.Map<FilmEntity>(filmCreate);
        
        // find director and assign him to film
        var director = await _directorService.GetOrCreateDirectorAsync(filmCreate.DirectorPersonId);
        filmEntity.Director = director;
        
        filmEntity = await _filmRepository.CreateAsync(filmEntity);
        return _mapper.Map<FilmDto>(filmEntity);
    }

    public async Task<FilmDto?> UpdateFilmAsync(FilmUpdate filmUpdate)
    {
        var filmEntity = await GetFilmEntityOrThrowAsync(filmUpdate.FilmId);

        // update director
        if (filmEntity.Director.PersonId != filmUpdate.DirectorPersonId)
        {
            var newDirector = await _directorService.GetOrCreateDirectorAsync(filmUpdate.DirectorPersonId);
            filmEntity.Director = newDirector;
        }

        // map new values to entity
        _mapper.Map(filmUpdate, filmEntity);

        filmEntity = await _filmRepository.UpdateAsync(filmEntity);
        return _mapper.Map<FilmDto>(filmEntity);
    }

    public async Task<bool> DeleteFilmAsync(Guid filmId)
    {
        var filmEntity = await GetFilmEntityOrThrowAsync(filmId);
        return await _filmRepository.DeleteAsync(filmEntity);
    }

    public async Task<ICollection<CharacterMediaDto>> GetFilmCharactersAsync(Guid filmId)
    {
        var filmEntity = await GetFilmEntityOrThrowAsync(filmId);
        return await _characterService.GetCharactersForFilmOrShowAsync(filmId: filmEntity.Id);
    }

    public async Task<bool> AddGenreAsync(Guid filmId, Guid genreId)
    {
        var filmEntity = await GetFilmEntityOrThrowAsync(filmId);
        var genreEntity = await _genreRepository.FindByIdAsync(genreId);
        if (genreEntity is null)
        {
            throw new BadRequestException($"Genre {genreId} not found");
        }
        filmEntity.Genres.Add(genreEntity);
        filmEntity = await _filmRepository.UpdateAsync(filmEntity);
        return filmEntity is not null;
    }

    public async Task<bool> RemoveGenreAsync(Guid filmId, Guid genreId)
    {
        var filmEntity = await GetFilmEntityOrThrowAsync(filmId);
        var removed = filmEntity.Genres.RemoveAll(genre => genre.Id == genreId);
        if (removed == 0)
        {
            throw new BadRequestException($"Genre {genreId} not found");
        }
        
        filmEntity = await _filmRepository.UpdateAsync(filmEntity);
        return filmEntity is not null;
    }
    
    private async Task<FilmEntity> GetFilmEntityOrThrowAsync(Guid filmId) {
        var filmEntity = await _filmRepository.FindByIdAsync(filmId);
        if (filmEntity is null)
        {
            throw new NotFoundException($"Film with id {filmId} not found.");
        }

        return filmEntity;
    }
}