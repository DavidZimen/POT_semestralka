using Domain.Entity;

namespace Persistence.Repositories;

public interface IPersonRepository : IBaseRepository<PersonEntity, Guid>;

public class PersonRepository : BaseRepository<PersonEntity, Guid>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}