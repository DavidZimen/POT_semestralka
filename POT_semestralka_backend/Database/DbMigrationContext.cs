using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POT_semestralka_backend.Models;

namespace POT_semestralka_backend.Database;

public class DbMigrationContext : DbContext
{
    private readonly ILogger<DbMigrationContext> _logger;
    
    public DbMigrationContext(
        DbContextOptions<DbMigrationContext> options, 
        [FromServices] ILogger<DbMigrationContext> logger
        ) : base(options)
    {
        _logger = logger;
    }

    public DbSet<Product> Products { get; init; }
    public DbSet<Hotel> Hotels { get; init; }

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