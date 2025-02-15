using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communication with repository related to Actor data.
/// </summary>
public interface IActorService : IService
{
    /// <summary>
    /// Creates new Actor with provided base on provided personId, or if exist it retrieves its entity.
    /// </summary>
    Task<ActorEntity> GetOrCreateActorAsync(Guid personId);
    
    /// <summary>
    /// Retrieves list of all actors.
    /// </summary>
    Task<ICollection<ActorDto>> GetActorsAsync();
    
    /// <param name="actorId">ID of the actor, that should be queried.</param>
    /// <returns>List of shows and films with characters the actor played.</returns>
    Task<ICollection<CharacterActorDto>> GetActorCharacters(Guid actorId);
}
    
public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public ActorService(IActorRepository actorRepository, IMapper mapper, IPersonRepository personRepository)
    {
        _actorRepository = actorRepository;
        _mapper = mapper;
        _personRepository = personRepository;
    }

    public async Task<ActorEntity> GetOrCreateActorAsync(Guid personId)
    {
        var actorEntity = await _actorRepository.GetActorByPersonIdAsync(personId);
        if (actorEntity is not null) return actorEntity;
        
        actorEntity = await _actorRepository.CreateAsync(new ActorEntity
        {
            PersonId = personId,
            Person = await _personRepository.FindByIdAsync(personId) ?? throw new BadRequestException($"Person with id {personId} was not found.")
        });
        return actorEntity;
    }

    public async Task<ICollection<ActorDto>> GetActorsAsync()
    {
        return (await _actorRepository.GetAllAsync())
            .Select(_mapper.Map<ActorDto>)
            .ToList();
    }

    public async Task<ICollection<CharacterActorDto>> GetActorCharacters(Guid actorId)
    {
        var actor = await _actorRepository.FindByIdAsync(actorId);
        if (actor is null)
        {
            throw new NotFoundException($"Actor with id {actorId} not found");
        }
        
        return actor.Characters
            .Where(characterEntity => characterEntity.Film is not null || characterEntity.Show is not null)
            .Select(_mapper.Map<CharacterActorDto>)
            .ToList();
    }
}