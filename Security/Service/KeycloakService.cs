using Flurl.Http;
using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.RealmsAdmin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Security.Options;

namespace Security.Service;

public class KeycloakService : IKeycloakService
{
    private readonly ILogger<KeycloakService> _logger;
    
    private readonly KeycloakClient _keycloakClient;
    
    private readonly KeycloakOwnOptions _keycloakOptions;

    public KeycloakService(
        KeycloakClient keycloakClient, 
        IOptions<KeycloakOwnOptions> keycloakOptions, 
        ILogger<KeycloakService> logger)
    {
        _keycloakClient = keycloakClient;
        _logger = logger;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async void CreateRealmIfNotExists()
    {
        try
        {
            // Try to get the realm
            var realm = await _keycloakClient.GetRealmAsync(_keycloakOptions.Realm);
            _logger.LogInformation($"Realm with name {realm.DisplayName} already exists.");
        }
        catch (FlurlHttpException ex) when (ex.StatusCode == 404) // If the realm does not exist, catch 404
        {
            CreateRealm();
        }
        catch (FlurlHttpException ex)
        {
            // Log any other FlurlHttpExceptions besides 404 (e.g., 500 server errors)
            var message = $"Error retrieving realm with name {_keycloakOptions.Realm}: {ex.Message}";
            _logger.LogCritical(message);
            throw new ApplicationException(message);
        }
    }

    public void CreateClientIfNotExists()
    {
        throw new NotImplementedException();
    }

    private async void CreateRealm()
    {
        _logger.LogInformation($"Realm with name {_keycloakOptions.Realm} not found. Creating...");
        try
        {
            // Attempt to create/import the realm
            var result = await _keycloakClient.ImportRealmAsync(_keycloakOptions.Realm, PrepareRealmRepresentation());
            if (result)
            {
                _logger.LogInformation($"Realm with name {_keycloakOptions.Realm} has been successfully created.");
            }
            else
            {
                var message = $"Failed to create realm with name {_keycloakOptions.Realm}.";
                _logger.LogCritical(message);
                throw new ApplicationException(message);
            }
        }
        catch (FlurlHttpException importEx)
        {
            var message = $"Error creating realm with name {_keycloakOptions.Realm}: {importEx.Message}";
            _logger.LogCritical(message);
            throw new ApplicationException(message);
        }
    }

    private Realm PrepareRealmRepresentation()
    {
        return new Realm
        {
            Id = _keycloakOptions.Realm.ToLower(),
            _Realm = _keycloakOptions.Realm.ToLower(),
            DisplayName = $"{char.ToUpper(_keycloakOptions.Realm[0])}{_keycloakOptions.Realm[1..]}",
            Enabled = true,
            RegistrationAllowed = true,
            RegistrationEmailAsUsername = true,
            BruteForceProtected = true,
            AccessTokenLifespan = _keycloakOptions.AccessTokenLifeSpan * 60, // Convert minutes to seconds
            RememberMe = true,
            RefreshTokenMaxReuse = 30,
            RevokeRefreshToken = true,
            VerifyEmail = true,
            DuplicateEmailsAllowed = false,
            ResetPasswordAllowed = true,
            PermanentLockout = true,
            RequiredCredentials = new List<string> { "password" }
        };
    }

    private void CreateClient()
    {
        var client = new Client
        {
            ClientId = _keycloakOptions.ClientName,
            Name = _keycloakOptions.ClientName,
            PublicClient = false,
            StandardFlowEnabled = true,
            ImplicitFlowEnabled = true,
            Enabled = true,
            DirectAccessGrantsEnabled = true
            
        };
        // _keycloakClient.CreateClientAsync()
    }
}