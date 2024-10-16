using Flurl.Http;
using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Security.Options;

namespace Security.Service;

public class KeycloakService : IKeycloakService
{
    private readonly ILogger<KeycloakService> _logger;
    
    private readonly KeycloakClient _keycloakClient;
    
    private readonly KeycloakOwnOptions _keycloakOwnOptions;

    public KeycloakService(
        KeycloakClient keycloakClient, 
        IOptions<KeycloakOwnOptions> keycloakOptions, 
        ILogger<KeycloakService> logger)
    {
        _keycloakClient = keycloakClient;
        _logger = logger;
        _keycloakOwnOptions = keycloakOptions.Value;
    }

    public async void CreateRealmIfNotExists()
    {
        try
        {
            var realm = await _keycloakClient.GetRealmAsync(_keycloakOwnOptions.Realm);
            _logger.LogInformation($"Realm with name {realm.DisplayName} already exists.");
        }
        catch (FlurlHttpException e)
        {
            _logger.LogInformation($"Creating realm with name {_keycloakOwnOptions.Realm}...");
            var realm = new Realm();
            // TODO make call to create realm with keycloak
        }
    }

    public void CreateClientIfNotExists()
    {
        throw new NotImplementedException();
    }
}