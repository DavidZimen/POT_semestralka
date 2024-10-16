using Keycloak.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using KeycloakOptions = Security.Options.KeycloakOptions;

namespace Security.Service;

public class KeycloakService : IKeycloakService
{
    private readonly ILogger<KeycloakService> _logger;
    
    private readonly KeycloakClient _keycloakClient;
    
    private readonly KeycloakOptions _keycloakOptions;

    public KeycloakService(
        KeycloakClient keycloakClient, 
        IOptions<KeycloakOptions> keycloakOptions, 
        ILogger<KeycloakService> logger)
    {
        _keycloakClient = keycloakClient;
        _logger = logger;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async void CreateRealmIfNotExists()
    {
        var realm = await _keycloakClient.GetRealmAsync(_keycloakOptions.Realm);    
        _logger.LogInformation($"Realm: {realm}");
    }

    public void CreateClientIfNotExists()
    {
        throw new NotImplementedException();
    }
}