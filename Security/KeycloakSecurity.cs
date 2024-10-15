using Keycloak.Net;
using Microsoft.Extensions.DependencyInjection;
using Security.Setup;

namespace Security;

public static class KeycloakSecurity
{
    public static IServiceCollection ConfigureKeycloakServices(this IServiceCollection services)
    {
        // map options from appsettings.json
        services.AddOptions<KeycloakOptions>().BindConfiguration(nameof(KeycloakOptions));
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<SwaggerGenOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        
        return services;
    }

    public static IServiceCollection AddKeycloak(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication()
            .AddJwtBearer();

        return services;
    }
}