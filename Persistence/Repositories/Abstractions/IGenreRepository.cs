using Domain.Entity;

namespace Persistence.Repositories.Abstractions;

/// <summary>
/// Repository for performing database operation related to the <seealso cref="GenreEntity"/>
/// </summary>
public interface IGenreRepository : IBaseRepository<GenreEntity, Guid>
{
    
    /// <summary>
    /// Retrieves GenreEntity from DB by name.
    /// This name should be unique.
    /// </summary>
    Task<GenreEntity?> GetGenreByName(string name);
}