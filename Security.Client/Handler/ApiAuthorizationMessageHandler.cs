using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;

namespace Security.Client.Handler;

/// <summary>
/// Message handler for adding security token to the header when making
/// HTTP requests to the application Web API.
/// </summary>
public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public ApiAuthorizationMessageHandler(
        IAccessTokenProvider provider, 
        NavigationManager navigation,
        IConfiguration configuration
        ) : base(provider, navigation)
    {
        ConfigureHandler(authorizedUrls: [configuration["ApiUrl"] ?? navigation.BaseUri]!);
    }
}