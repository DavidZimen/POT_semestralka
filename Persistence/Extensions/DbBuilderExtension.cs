using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Interceptors;
using Persistence.Options;
using Persistence.Repositories.Abstractions;

namespace Persistence.Extensions;

public static class DbBuilderExtension
{
    private const string RepositoriesNameSpace = "Persistence.Repositories";
    
    public static IHostApplicationBuilder ConfigureDatabase(
        this IHostApplicationBuilder builder,
        Action<DbConfigOptions>? options = default)
    {
        var conn = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddSingleton<AuditableEntityInterceptor>();
        builder.Services.AddDbContext<ApplicationDbContext>(
                (sp, opt) => opt
                    .UseNpgsql(conn, x => x.MigrationsHistoryTable("__EFMigrationsHistory", ApplicationDbContext.ApplicationSchema))
                    .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>())
                    .UseLazyLoadingProxies())
            .AddMigrationService(options);
        return builder;
    }

    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        typeof(IRepository).Assembly
            .GetTypes()
            .Where(type => type.Namespace == RepositoriesNameSpace)
            .Where(type => type.GetInterface(nameof(IRepository)) is not null)
            .Where(type => type is { IsClass: true, IsAbstract: false, IsInterface: false })
            .ToList()
            .ForEach(type => builder.Services.AddScoped(type.GetInterface($"I{type.Name}")!, type));
    }
}