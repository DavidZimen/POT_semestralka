using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for communication with repository related to Character data.
/// </summary>
public interface ICharacterService : IService
{
    /// <summary>
    /// Finds all characters, or filters them based on provided parameters.
    /// </summary>
    /// <param name="filmId">ID of the film for filtering.</param>
    /// <param name="showId">ID of the show for filtering.</param>
    Task<ICollection<CharacterDto>> GetCharactersAsync(Guid? filmId, Guid? showId);
    
    /// <summary>
    /// Creates a new character for film or show with provided person as actor.
    /// </summary>
    /// <param name="characterCreate">Information about character.</param>
    /// <returns>Created character.</returns>
    Task<CharacterDto> CreateCharacterAsync(CharacterCreate characterCreate);
    
    /// <summary>
    /// Deletes character from DB based on provided ID.
    /// </summary>
    /// <param name="characterId"></param>
    /// <returns></returns>
    Task<bool> DeleteCharacterAsync(Guid characterId);
    
    /// <summary>
    /// Retrieves characters, that belong to the film with provided ID.
    /// </summary>
    Task<ICollection<CharacterMediaDto>> GetCharactersForFilmAsync(Guid filmId);
    
    /// <summary>
    /// Retrieves characters, that belong to the show with provided ID.
    /// </summary>
    Task<ICollection<CharacterMediaDto>> GetCharactersForShowAsync(Guid showId);
}

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly IActorService _actorService;
    private readonly IMapper _mapper;

    public CharacterService(ICharacterRepository characterRepository, IMapper mapper, IActorService actorService, IFilmRepository filmRepository)
    {
        _characterRepository = characterRepository;
        _actorService = actorService;
        _filmRepository = filmRepository;
        _mapper = mapper;
    }

    public async Task<CharacterDto> CreateCharacterAsync(CharacterCreate characterCreate)
    {
        if (characterCreate.FilmId is null && characterCreate.ShowId is null)
        {
            throw new BadRequestException("Please provide film or show id for this character.");
        }
        
        var characterEntity = new CharacterEntity
        {
            Name = characterCreate.CharacterName,
            Actor = await _actorService.GetOrCreateActorAsync(characterCreate.ActorPersonId)
        };

        if (characterCreate.FilmId.HasValue)
        {
            var filmEntity = await _filmRepository.FindByIdAsync(characterCreate.FilmId.Value);
            if (filmEntity is null)
            {
                throw new BadRequestException("Film could not be found.");
            }
            characterEntity.Film = filmEntity;
        }
        else
        {
            //TODO do it also for the show, when show repository is finished
        }
        characterEntity = await _characterRepository.CreateAsync(characterEntity);
        return _mapper.Map<CharacterDto>(characterEntity);
    }

    public async Task<bool> DeleteCharacterAsync(Guid characterId)
    {
        var characterEntity = await _characterRepository.FindByIdAsync(characterId);
        if (characterEntity is null)
        {
            throw new NotFoundException($"Character with id {characterId} does not exist.");
        }
        return await _characterRepository.DeleteAsync(characterEntity);
    }

    public async Task<ICollection<CharacterMediaDto>> GetCharactersForFilmAsync(Guid filmId)
    {
        return (await _characterRepository.GetCharactersAsync(filmId: filmId))
            .Select(_mapper.Map<CharacterMediaDto>)
            .ToList();
    }

    public async Task<ICollection<CharacterMediaDto>> GetCharactersForShowAsync(Guid showId)
    {
        return (await _characterRepository.GetCharactersAsync(showId: showId))
            .Select(_mapper.Map<CharacterMediaDto>)
            .ToList();
    }

    public async Task<ICollection<CharacterDto>> GetCharactersAsync(Guid? filmId, Guid? showId)
    {
        return (await _characterRepository.GetCharactersAsync(filmId, showId))
            .Select(_mapper.Map<CharacterDto>)
            .ToList();
    }
}