using System.IdentityModel.Tokens.Jwt;
using Keycloak.Net;
using Microsoft.Extensions.DependencyInjection;
using Security.Config;
using Security.Service;
using Security.Setup;

namespace Security;

public static class KeycloakSecurity
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
        
        services.AddSingleton<KeycloakClient>(sp =>
        {
            var options = sp.GetRequiredService<Options.KeycloakOptions>();
            ArgumentNullException.ThrowIfNull(options);

            return new KeycloakClient(options.Url, options.Username, options.Password);
        });
        services.AddSingleton<IKeycloakService, KeycloakService>();
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
}