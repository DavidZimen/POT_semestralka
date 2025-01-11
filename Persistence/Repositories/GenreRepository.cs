using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

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