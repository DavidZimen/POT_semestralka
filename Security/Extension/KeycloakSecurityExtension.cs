using System.IdentityModel.Tokens.Jwt;
using Keycloak.Net;
using Microsoft.Extensions.DependencyInjection;
using Security.Config;
using Security.Service;
using Security.Setup;

namespace Security.Extension;

public static class KeycloakSecurityExtension
{
    public static IServiceCollection ConfigureKeycloakForApi(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
        services.AddOptions<KeycloakOptions>().BindConfiguration(nameof(KeycloakOptions));
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<SwaggerGenOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.ConfigureOptions<AuthorizationOptionsSetup>();
        services.ConfigureOptions<CookieAuthenticationOptionsSetup>();
        
        services.AddKeycloakServices();
        
        return services;
    }

    public static IServiceCollection ConfigureKeycloakServer(this IServiceCollection services)
    {
        services.AddKeycloakServices();
        services.AddHostedService<KeycloakServerConfiguration>();
        return services;
    }

    public static IServiceCollection AddKeycloakToApi(this IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddCookie()
            .AddJwtBearer();
        
        services.AddAuthorization();

        return services;
    }

    private static void AddKeycloakServices(this IServiceCollection services)
    {
        if (!services.IsServiceRegistered<KeycloakClient>())
        {
            services.AddSingleton<KeycloakClient>(sp =>
            {
                var options = sp.GetRequiredService<Options.KeycloakOptions>();
                ArgumentNullException.ThrowIfNull(options);

                return new KeycloakClient(options.Url, options.Username, options.Password);
            });
        }

        if (!services.IsServiceRegistered<IKeycloakService, KeycloakService>())
        {
            services.AddSingleton<IKeycloakService, KeycloakService>();
        }
    }
    
    public static bool IsServiceRegistered<TService>(this IServiceCollection services)
    {
        return services.Any(s => s.ServiceType == typeof(TService));
    }
    
    private static bool IsServiceRegistered<TService, TImplementation>(this IServiceCollection services)
    {
        return services.Any(s => s.ServiceType == typeof(TService) && s.ImplementationType == typeof(TImplementation));
    }
}