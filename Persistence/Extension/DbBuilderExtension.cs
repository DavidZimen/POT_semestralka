using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Options;

namespace Persistence.Extension;

public static class DbBuilderExtension
{
    public static void ConfigureDatabase(
        this IHostApplicationBuilder builder,
        Action<DbConfigOptions>? options = default)
    {
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(o => 
                o.UseNpgsql(conn, x => 
                    x.MigrationsHistoryTable("__EFMigrationsHistory", ApplicationDbContext.ApplicationSchema)
                )
            )
            .AddMigrationService(options);
    }
}