using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;

namespace Persistence;

/// <summary>
/// Own DbContext for providing connection to the PostgreSQL database
/// and retrieving data for API.
/// </summary>
public sealed class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Schema in database where data will be persisted.
    /// </summary>
    public const string ApplicationSchema = "application_schema";
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(ApplicationSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyDeletionQueryFilterToEntities();
    }
}