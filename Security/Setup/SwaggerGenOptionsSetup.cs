using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Security.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Security.Setup;

public class SwaggerGenOptionsSetup : IConfigureOptions<SwaggerGenOptions>
{
    private const string SecuritySchemeName = "Keycloak";

    private readonly KeycloakOwnOptions _keycloakOptions;

    public SwaggerGenOptionsSetup(IOptions<KeycloakOwnOptions> keycloakOptions)
    {
        _keycloakOptions = keycloakOptions.Value;
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
                    AuthorizationUrl = new Uri(_keycloakOptions.AuthorizationUrl),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "openid" },
                        { "profile", "profile" },
                        { "email", "email" }
                    }
                }
            }
        };
        o.AddSecurityDefinition(SecuritySchemeName,securityDefinition );

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
                    In = ParameterLocation.Header,
                    Name = "Bearer",
                    Scheme = "Bearer"
                },
                []
            }
        };
        o.AddSecurityRequirement(securityRequirement);
    }
}