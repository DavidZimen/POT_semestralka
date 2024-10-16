using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Migrations.Service;

namespace Persistence.Extension;

public static class DbMigrationExtension
{
    public static void AddMigrationService(this IServiceCollection services, bool runMigrations)
    {
        services.AddSingleton<IDbMigrationService>(sp =>
        {
            var migrationService = new DbMigrationService(
                sp.GetRequiredService<ApplicationDbContext>(),
                sp.GetRequiredService<ILogger<DbMigrationService>>());
            if (runMigrations)
            {
                migrationService.RunMigrations();
            }
            return migrationService;
        });
    }
}