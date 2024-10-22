using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Options;

namespace Persistence.Migrations.Service;

internal sealed class DbMigrationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    
    private readonly ILogger<DbMigrationService> _logger;

    private readonly DbConfigOptions _dbConfigOptions;
    
    public DbMigrationService(IServiceProvider serviceProvider, IOptions<DbConfigOptions> migrationsOptions, ILogger<DbMigrationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _dbConfigOptions = migrationsOptions.Value;
    }
    
    private void RunMigrations()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        _logger.LogInformation("Beginning migration...");
        var appliedMigrations = dbContext.Database.GetAppliedMigrations();
        foreach (var appliedMigration in appliedMigrations)
        {
            _logger.LogInformation(appliedMigration);
        }
        
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migration found -> EXECUTING...");
            dbContext.Database.Migrate();
        }
        else
        {
            _logger.LogInformation("Migration NOT found -> SKIPPING.");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (_dbConfigOptions.RunMigrationsOnStartup)
        {
            RunMigrations();
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}