using Microsoft.Extensions.DependencyInjection;
using Persistence.Migrations.Options;
using Persistence.Migrations.Service;

namespace Persistence.Extension;

public static class DbMigrationExtension
{
    public static void AddMigrationService(this IServiceCollection services, Action<MigrationsOptions>? options = default)
    {
        services.Configure(options ?? (migrationsOptions => { migrationsOptions.RunMigrationsOnStartup = false; }) );
        services.AddHostedService<DbMigrationService>();
    }
}