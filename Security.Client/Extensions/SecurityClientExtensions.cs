
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Constants;

namespace Security.Client.Extensions;

public static class SecurityClientExtensions
{
    public static WebAssemblyHostBuilder AddKeycloakToClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddOidcAuthentication(o =>
        {
            var keycloakSection = builder.Configuration.GetRequiredSection(SecurityConstants.KeycloakOptionsName);
            
            o.ProviderOptions.MetadataUrl = keycloakSection["MetadataAddress"] ?? throw new InvalidOperationException();
            o.ProviderOptions.ClientId = keycloakSection["ClientName"];
            o.ProviderOptions.Authority = keycloakSection["ValidIssuer"];
            o.ProviderOptions.ResponseType = "id_token token";
            o.ProviderOptions.DefaultScopes.Add("openid");
            o.ProviderOptions.DefaultScopes.Add("profile");
            o.ProviderOptions.DefaultScopes.Add("email");

            o.UserOptions.NameClaim = SecurityConstants.UsernameClaim;
            o.UserOptions.RoleClaim = "role";
        });

        return builder;
    }
}