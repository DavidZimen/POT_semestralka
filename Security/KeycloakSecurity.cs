using System.IdentityModel.Tokens.Jwt;
using Keycloak.Net;
using Microsoft.Extensions.DependencyInjection;
using Security.Setup;

namespace Security;

public static class KeycloakSecurity
{
    public static IServiceCollection ConfigureKeycloak(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        
        services.AddOptions<KeycloakOptions>().BindConfiguration(nameof(KeycloakOptions));
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<SwaggerGenOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.ConfigureOptions<AuthorizationOptionsSetup>();
        services.ConfigureOptions<CookieAuthenticationOptionsSetup>();
        
        return services;
    }

    public static IServiceCollection AddKeycloak(this IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddCookie()
            .AddJwtBearer();
        
        services.AddAuthorization();

        return services;
    }
}