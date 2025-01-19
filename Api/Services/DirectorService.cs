using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communication with repository related to Director data.
/// </summary>
public interface IDirectorService : IService
{
    /// <summary>
    /// Creates new Director with provided base on provided personId, or if exist it retrieves its entity.
    /// </summary>
    Task<DirectorEntity> GetOrCreateDirectorAsync(Guid personId);
    
    /// <param name="directorId">ID of the director for finding films.</param>
    /// <returns>Collection of films, that director directed.</returns>
    Task<ICollection<DirectorFilmDto>> GetDirectorFilmsAsync(Guid directorId);
    
    /// <param name="directorId">ID of the director for finding shows.</param>
    /// <returns>Collection of shows, that director directed.</returns>
    Task<ICollection<DirectorShowDto>> GetDirectorShowsAsync(Guid directorId);
}

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public DirectorService(IDirectorRepository directorRepository, IMapper mapper, IPersonRepository personRepository)
    {
        _directorRepository = directorRepository;
        _mapper = mapper;
        _personRepository = personRepository;
    }

    public async Task<DirectorEntity> GetOrCreateDirectorAsync(Guid personId)
    {
        var directorEntity = await _directorRepository.GetDirectorByPersonIdAsync(personId);
        if (directorEntity is not null) return directorEntity;
        
        directorEntity = await _directorRepository.CreateAsync(new DirectorEntity
        {
            PersonId = personId,
            Person = await _personRepository.FindByIdAsync(personId) ?? throw new BadRequestException($"Person with id {personId} was not found.")
        });
        return directorEntity;
    }

    public async Task<ICollection<DirectorFilmDto>> GetDirectorFilmsAsync(Guid directorId)
    {
        var directorEntity = await FindOrThrowAsync(directorId);
        return directorEntity.Films
            .Select(_mapper.Map<DirectorFilmDto>)
            .ToList();
    }

    public async Task<ICollection<DirectorShowDto>> GetDirectorShowsAsync(Guid directorId)
    {
        var directorEntity = await FindOrThrowAsync(directorId);
        return directorEntity.Episodes
            .Select(episode => episode.Season.Show)
            .DistinctBy(show => show.Id)
            .Select(showEntity =>
            {
                var directorShowDto = _mapper.Map<DirectorShowDto>(showEntity);
                directorShowDto.EpisodeCount = directorEntity.Episodes
                    .Count(episode => episode.Season.ShowId == directorShowDto.ShowId);
                return directorShowDto;
            }).ToList();
    }

    private async Task<DirectorEntity> FindOrThrowAsync(Guid directorId)
    {
        var directorEntity = await _directorRepository.FindByIdAsync(directorId);
        if (directorEntity is null)
            throw new NotFoundException($"Director with id {directorId} not found");
        return directorEntity;
    }
}