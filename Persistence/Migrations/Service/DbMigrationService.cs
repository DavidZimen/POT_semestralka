using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Migrations.Service;

public class DbMigrationService : IDbMigrationService
{
    private readonly DbContext _dbContext;
    
    private readonly ILogger<DbMigrationService> _logger;

    public DbMigrationService(ApplicationDbContext dbContext, ILogger<DbMigrationService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void RunMigrations()
    {
        _logger.LogInformation("Beginning migration...");
        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migration found -> EXECUTING...");
            _dbContext.Database.Migrate();
        }
        else
        {
            _logger.LogInformation("Migration NOT found -> SKIPPING.");
        }
    }
}