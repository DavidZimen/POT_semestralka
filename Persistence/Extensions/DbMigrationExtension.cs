using Microsoft.Extensions.DependencyInjection;
using Persistence.Migrations.Service;
using Persistence.Options;

namespace Persistence.Extensions;

public static class DbMigrationExtension
{
    public static void AddMigrationService(this IServiceCollection services, Action<DbConfigOptions>? options = default)
    {
        services.Configure(options ?? (dbConfigOptions => { dbConfigOptions.RunMigrationsOnStartup = true; }) );
        services.AddHostedService<DbMigrationService>();
    }
}