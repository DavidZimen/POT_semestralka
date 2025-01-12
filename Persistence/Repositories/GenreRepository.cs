using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

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

public class GenreRepository : BaseRepository<GenreEntity, Guid>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<GenreEntity?> GetGenreByName(string name)
    {
        return DbSet.FirstOrDefaultAsync(genre => genre.Name == name);
    }
}