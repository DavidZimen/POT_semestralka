using System.IdentityModel.Tokens.Jwt;
using Keycloak.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Security.Options;
using Security.Service;
using Security.Setup;

namespace Security.Config;

public static class KeycloakSecurityConfiguration
{
    private const string KeycloakOptionsName = "Keycloak";
    
    public static IServiceCollection ConfigureKeycloakForApi(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
        services.AddOptions<KeycloakOwnOptions>().BindConfiguration(KeycloakOptionsName);
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<SwaggerGenOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.ConfigureOptions<AuthorizationOptionsSetup>();
        
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
        services.AddAuthentication()
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
                var options = sp.GetRequiredService<IOptions<KeycloakOwnOptions>>().Value;
                ArgumentNullException.ThrowIfNull(options);
                return new KeycloakClient(options.Url, options.Username, options.Password, new KeycloakOptions(authenticationRealm:"master"));
            });
        }

        if (!services.IsServiceRegistered<IKeycloakService, KeycloakService>())
        {
            services.AddSingleton<IKeycloakService, KeycloakService>();
        }
    }
    
    private static bool IsServiceRegistered<TService>(this IServiceCollection services)
    {
        return services.Any(s => s.ServiceType == typeof(TService));
    }
    
    private static bool IsServiceRegistered<TService, TImplementation>(this IServiceCollection services)
    {
        return services.Any(s => s.ServiceType == typeof(TService) && s.ImplementationType == typeof(TImplementation));
    }
}