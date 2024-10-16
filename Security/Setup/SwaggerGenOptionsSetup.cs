using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Security.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Security.Setup;

public class SwaggerGenOptionsSetup : IConfigureOptions<SwaggerGenOptions>
{
    private const string SecuritySchemeName = "Keycloak";

    private readonly KeycloakOwnOptions _keycloakOwnOptions;

    public SwaggerGenOptionsSetup(IOptions<KeycloakOwnOptions> keycloakOptions)
    {
        _keycloakOwnOptions = keycloakOptions.Value;
    }

    public void Configure(SwaggerGenOptions o)
    {
        o.CustomSchemaIds(id => id.FullName!.Replace("+", "-"));

        var securityDefinition = new OpenApiSecurityScheme 
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(_keycloakOwnOptions.AuthorizationUrl),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "openid" },
                        { "profile", "profile" }
                    }
                }
            }
        };
        o.AddSecurityDefinition("Keycloak",securityDefinition );

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = SecuritySchemeName,
                        Type = ReferenceType.SecurityScheme
                    },
                    In = ParameterLocation.Cookie,
                    Name = "access-token",
                    Scheme = "oauth2"
                },
                []
            }
        };
        o.AddSecurityRequirement(securityRequirement);
    }
}