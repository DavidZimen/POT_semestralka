using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Interceptors;
using Persistence.Options;
using Persistence.Repository;
using Persistence.Repository.Abstractions;

namespace Persistence.Extensions;

public static class DbBuilderExtension
{
    public static IHostApplicationBuilder ConfigureDatabase(
        this IHostApplicationBuilder builder,
        Action<DbConfigOptions>? options = default)
    {
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddSingleton<AuditableEntityInterceptor>();
        builder.Services.AddDbContext<ApplicationDbContext>(
                (sp, opt) => opt
                    .UseNpgsql(conn, x => x.MigrationsHistoryTable("__EFMigrationsHistory", ApplicationDbContext.ApplicationSchema))
                    .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>()))
            .AddMigrationService(options);
        return builder;
    }

    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
    }
}