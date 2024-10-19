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
    private const string RolesClientScope = "roles";
    private const string RolesMapper = "realm roles";
    private const string RolesClaimConfigProperty = "claim.name";
    private const string NewRoleClaimName = "roles";
    
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

    public async Task CreateRealmIfNotExists()
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

    public async Task CreateClientIfNotExists()
    {
        try
        {
            var clientExists = (await _keycloakClient.GetClientsAsync(_keycloakOptions.Realm))
                .Select(client => client.ClientId == _keycloakOptions.ClientName)
                .Any();

            if (clientExists)
            {
                _logger.LogInformation($"Client with id {_keycloakOptions.ClientName} already exists.");
                return;
            }
            
            CreateClient();
        }
        catch (FlurlHttpException e) when (e.StatusCode == 404)
        {
            CreateClient();
        }
        catch (FlurlHttpException ex)
        {
            // Log any other FlurlHttpExceptions besides 404 (e.g., 500 server errors)
            var message = $"Error retrieving cliend with id {_keycloakOptions.ClientName}: {ex.Message}";
            _logger.LogCritical(message);
            throw new ApplicationException(message);
        }

        // try to retrieve client secret for authentication
        // try
        // {
        //     var credentials = await _keycloakClient.GetClientSecretAsync(_keycloakOptions.Realm, _keycloakOptions.ClientName);
        //     _keycloakOptions.ClientSercet = credentials.Value;
        // }
        // catch (FlurlHttpException e)
        // {
        //     _logger.LogError($"Unable to retrieve client secret {_keycloakOptions.ClientName}: {e.Message}");
        // }
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

    private async void CreateClient()
    {
        _logger.LogInformation($"Creating client with id {_keycloakOptions.ClientName}");
        try
        {
            var result = await _keycloakClient.CreateClientAsync(_keycloakOptions.Realm, PrepareClientRepresentation());
            if (result)
            {
                _logger.LogInformation($"Client with id {_keycloakOptions.ClientName} has been successfully created.");
            }
            else
            {
                var message = $"Failed to create cliend with id {_keycloakOptions.ClientName}.";
                _logger.LogCritical(message);
                throw new ApplicationException(message);
            }
        }
        catch (FlurlHttpException e)
        {
            var message = $"Error creating client with id {_keycloakOptions.ClientName}: {e.Message}";
            _logger.LogCritical(message);
            throw new ApplicationException(message);
        }
    }

    public async Task UpdateRolesClaimMapping()
    {
        // find scope for realm roles, throw exception if null
        var rolesScope = (await _keycloakClient.GetClientScopesAsync(_keycloakOptions.Realm))
            .First(clientScope => clientScope.Name == RolesClientScope);
        
        var rolesMapper = rolesScope.ProtocolMappers.First(mapper => mapper.Name == RolesMapper);
        if (rolesMapper.Config[RolesClaimConfigProperty] == NewRoleClaimName)
        {
            _logger.LogInformation($"Roles token claim mapper in realm {_keycloakOptions.Realm} already configured to {NewRoleClaimName}");
            return;
        }
        
        // try to update roles client scope
        try
        {
            rolesMapper.Config[RolesClaimConfigProperty] = NewRoleClaimName;
            var result = await _keycloakClient.UpdateProtocolMapperAsync(_keycloakOptions.Realm, rolesScope.Id, rolesMapper.Id, rolesMapper);
            if (result)
            {
                _logger.LogInformation($"Roles token claim mapper in realm {_keycloakOptions.Realm} configured to value {NewRoleClaimName}");
            }
            else
            {
                var message = $"Roles token claim mapper in realm {_keycloakOptions.Realm} configurtion failed. Cannot start application.";
                _logger.LogCritical(message);
                throw new ApplicationException(message);
            }
        }
        catch (FlurlHttpException e)
        {
            var message = $"Error updating client scope for roles token mapping: {e.Message}";
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

    private Client PrepareClientRepresentation()
    {
        return new Client
        {
            ClientId = _keycloakOptions.ClientName,
            Name = _keycloakOptions.ClientName,
            PublicClient = true,
            StandardFlowEnabled = true,
            ImplicitFlowEnabled = true,
            Enabled = true,
            DirectAccessGrantsEnabled = true,
            RedirectUris = new List<string> { "http://localhost:5000/*" },
            WebOrigins = new List<string> { "http://localhost:5000" }
        };
    }
}