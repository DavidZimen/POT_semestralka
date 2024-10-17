using Microsoft.EntityFrameworkCore;

namespace Persistence;

/// <summary>
/// Own DbContext for providing connection to the PostgreSQL database
/// and retrieving data for API.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Schema in database where data will be persisted.
    /// </summary>
    public const string ApplicationSchema = "application_schema";
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(ApplicationSchema);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}