using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Entity;

namespace Persistence;

public class ApplicationDbContext : IdentityDbContext<UserEntity>
{

    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) 
        : base(options)
    {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        // here goes the extensions for base data
        builder.AddRoles();
        builder.AddUsers();
        builder.AddUserRoles();
    }

    /// <summary>
    /// Function to be called in Program.cs class before application startup.
    /// Ensures, that the database is in correct state for application to run.
    /// </summary>
    public void Migrate()
    {
        _logger.LogInformation("Beginning migration...");
        if (Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migration found -> EXECUTING...");
            Database.Migrate();
        }
        else
        {
            _logger.LogInformation("Migration NOT found -> SKIPPING.");
        }
    }
}