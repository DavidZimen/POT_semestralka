using Domain.Entity;

namespace Persistence.Repositories;

/// <summary>
/// Repository for manipulating images in the DB.
/// </summary>
public interface IImageRepository : IBaseRepository<ImageEntity, Guid>;

public class ImageRepository : BaseRepository<ImageEntity, Guid>, IImageRepository
{
    public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}