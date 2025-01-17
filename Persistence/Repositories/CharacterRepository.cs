using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

/// <summary>
/// Repository for processing entertainment media characters in the application.
/// </summary>
public interface ICharacterRepository : IBaseRepository<CharacterEntity, Guid>
{
    /// <summary>
    /// Finds all characters in DB, or filters them based on provided paramaters.
    /// </summary>
    /// <param name="filmId">ID of the film for filtering.</param>
    /// <param name="showId">ID of the show for filtering.</param>
    Task<ICollection<CharacterEntity>> GetCharactersAsync(Guid? filmId = default, Guid? showId = default);
}

public class CharacterRepository : BaseRepository<CharacterEntity, Guid>, ICharacterRepository
{
    public CharacterRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<CharacterEntity>> GetCharactersAsync(Guid? filmId = default, Guid? showId = default)
    {
        var query = DbSet.AsQueryable();

        // Apply the filtering logic based on the search criteria
        if (filmId.HasValue)
        {
            query = query.Where(r => r.FilmId == filmId.Value);
        }
        if (showId.HasValue)
        {
            query = query.Where(r => r.ShowId == showId.Value);
        }

        return await query.ToListAsync();
    }
}