using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Options;

namespace Security.Setup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly KeycloakOwnOptions _keycloakOwnOptions;

    public JwtBearerOptionsSetup(IOptions<KeycloakOwnOptions> keycloakOptions)
    {
        _keycloakOwnOptions = keycloakOptions.Value;
    }

    public void Configure(JwtBearerOptions o)
    {
        o.RequireHttpsMetadata = false;
        o.Audience = _keycloakOwnOptions.Audience;
        o.MetadataAddress = _keycloakOwnOptions.MetadataAddress;
        
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _keycloakOwnOptions.ValidIssuer,
        };
    }
}