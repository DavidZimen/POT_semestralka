using Domain.Dto;

namespace Api.Services.Abstraction;

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
    Task<GenreDto?> UpdateGenreAsync(UpdateGenre updateGenre);
    
    /// <summary>
    /// Deletes genre with provided ID from DB.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteGenreAsync(Guid id);
}