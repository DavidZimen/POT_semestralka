using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

namespace Api.Services;

/// <summary>
/// Service for manipulating data for genres.
/// </summary>
public interface IGenreService : IService
{
    /// <summary>
    /// Retrieves Genre with provided ID.
    /// </summary>
    Task<GenreDto?> GetGenreByIdAsync(Guid id);
    
    /// <summary>
    /// Retrieves list of all genres from DB.
    /// </summary>
    Task<List<GenreDto>> GetGenresAsync();
    
    /// <summary>
    /// Creates new Genre with provided name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>ID of the genre if created successfully.</returns>
    Task<Guid?> CreateGenreAsync(string name);

    /// <summary>
    /// Updates the genre entity based on new requirements.
    /// </summary>
    Task<GenreDto?> UpdateGenreAsync(GenreUpdate genreUpdate);
    
    /// <summary>
    /// Deletes genre with provided ID from DB.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteGenreAsync(Guid id);
}

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IMapper mapper, IGenreRepository genreRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
    }
    
    public async Task<GenreDto?> GetGenreByIdAsync(Guid id)
    {
        var genreEntity = await _genreRepository.FindByIdAsync(id);
        return _mapper.Map<GenreDto>(genreEntity);
    }

    public async Task<List<GenreDto>> GetGenresAsync()
    {
        return (await _genreRepository.GetAllAsync())
            .Select(_mapper.Map<GenreDto>)
            .ToList();
    }

    public async Task<Guid?> CreateGenreAsync(string name)
    {
        var existGenre = await _genreRepository.GetGenreByName(name) is not null;
        if (existGenre)
        {
            throw new ConflictException($"Genre with name {name} already exists.");
        }
        
        var genreEntity = new GenreEntity { Name = name };
        return (await _genreRepository.CreateAsync(genreEntity)).Id;
    }

    public async Task<GenreDto?> UpdateGenreAsync(GenreUpdate genreUpdate)
    {
        var genreEntity = await _genreRepository.FindByIdAsync(genreUpdate.GenreId);
        if (genreEntity is null)
        {
            throw new NotFoundException($"Genre with id {genreUpdate.GenreId} does not exist.");
        }
        
        var existGenreWithName = await _genreRepository.GetGenreByName(genreUpdate.Name) is not null;
        if (existGenreWithName)
        {
            throw new ConflictException($"Genre with name {genreUpdate.Name} already exists.");
        }
        
        genreEntity.Name = genreUpdate.Name;
        return _mapper.Map<GenreDto>(await _genreRepository.UpdateAsync(genreEntity));
    }

    public async Task<bool> DeleteGenreAsync(Guid id)
    {
        var genreEntity = await _genreRepository.FindByIdAsync(id);
        if (genreEntity is null)
        {
            throw new NotFoundException($"Genre with id {id} does not exist.");
        }
        
        return await _genreRepository.DeleteAsync(genreEntity);
    }
}