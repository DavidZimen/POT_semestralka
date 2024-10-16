using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Options;

namespace Security.Setup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly KeycloakOptions _keycloakOptions;

    public JwtBearerOptionsSetup(IOptions<KeycloakOptions> keycloakOptions)
    {
        _keycloakOptions = keycloakOptions.Value;
    }

    public void Configure(JwtBearerOptions o)
    {
        o.RequireHttpsMetadata = false;
        o.Audience = _keycloakOptions.Audience;
        o.MetadataAddress = _keycloakOptions.MetadataAddress;
        
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _keycloakOptions.ValidIssuer,
        };
        
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies[CookieAuthenticationOptionsSetup.AuthCookieName];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    }
}