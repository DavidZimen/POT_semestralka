using Api.Exceptions;
using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories.Abstractions;

namespace Api.Services;

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
        return await _genreRepository.CreateAsync(genreEntity);
    }

    public async Task<GenreDto?> UpdateGenreAsync(UpdateGenre updateGenre)
    {
        var genreEntity = await _genreRepository.FindByIdAsync(updateGenre.GenreId);
        if (genreEntity is null)
        {
            throw new NotFoundException($"Genre with id {updateGenre.GenreId} does not exist.");
        }
        
        var existGenreWithName = await _genreRepository.GetGenreByName(updateGenre.Name) is not null;
        if (existGenreWithName)
        {
            throw new ConflictException($"Genre with name {updateGenre.Name} already exists.");
        }
        
        genreEntity.Name = updateGenre.Name;
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