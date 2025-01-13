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
    /// Retrieves film by its ID from database.
    /// </summary>
    /// <param name="filmId">Film ID for the query.</param>
    /// <returns>Film with ID or null.</returns>
    Task<FilmDto?> GetFilmByIdAsync(Guid filmId);
    
    /// <summary>
    /// Creates a new Film in DB based on provided film parameter.
    /// </summary>
    /// <param name="film">Object with requested film specification.</param>
    /// <returns>ID of newly created film entity.</returns>
    Task<FilmDto?> CreateFilm(FilmCreate film);
}

public class FilmService : IFilmService
{
    private readonly IFilmRepository _filmRepository;
    private readonly IDirectorService _directorService;
    private readonly IActorService _actorService;
    private readonly IMapper _mapper;

    public FilmService(IFilmRepository filmRepository, IDirectorService directorService, IActorService actorService, IMapper mapper)
    {
        _filmRepository = filmRepository;
        _directorService = directorService;
        _actorService = actorService;
        _mapper = mapper;
    }

    public async Task<FilmDto?> GetFilmByIdAsync(Guid filmId)
    {
        var film = await _filmRepository.FindByIdAsync(filmId);
        return film is not null ? _mapper.Map<FilmDto>(film) : null;
    }

    public async Task<FilmDto?> CreateFilm(FilmCreate film)
    {
        var filmEntity = _mapper.Map<FilmEntity>(film);
        
        // find director and assign him to film
        var director = await _directorService.GetOrCreateDirectorAsync(film.DirectorPersonId);
        filmEntity.Director = director;
        
        filmEntity = await _filmRepository.CreateAsync(filmEntity);
        return _mapper.Map<FilmDto>(filmEntity);
    }
}