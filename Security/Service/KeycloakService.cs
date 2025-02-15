﻿using Flurl.Http;
using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.ProtocolMappers;
using Keycloak.Net.Models.RealmsAdmin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Security.Constants;
using Security.Exception;
using Security.Options;

namespace Security.Service;

/// <summary>
/// Service for communicationg with running Keycloak instance.
/// </summary>
public interface IKeycloakService
{
    /// <summary>
    /// Creates and configures new realm for storing information about users for this application.
    /// </summary>
    internal Task CreateRealmIfNotExists();

    /// <summary>
    /// Creates and configures client inside Keycloak realm, that will be responsible for issuing
    /// JWT tokens for authorization.
    /// </summary>
    internal Task CreateClientIfNotExists();

    /// <summary>
    /// Updates mapping of roles claim in JWT token from realm_access.roles to roles.
    /// </summary>
    internal Task UpdateRolesClaimMapping();
}

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

    public async Task CreateRealmIfNotExists()
    {
        try
        {
            var realm = await _keycloakClient.GetRealmAsync(_keycloakOptions.Realm);
            _logger.LogInformation("Realm with name {Realm} already exists.", realm.DisplayName);
        }
        catch (FlurlHttpException ex) when (ex.StatusCode == 404) // If the realm does not exist, catch 404
        {
            await CreateRealm();
        }
        catch (FlurlHttpException ex)
        {
            // Log any other FlurlHttpExceptions besides 404 (e.g., 500 server errors)
            var message = $"Error retrieving realm with name {_keycloakOptions.Realm}: {ex.Message}";
            _logger.LogCritical(message);
            throw new KeycloakInitializationException(message);
        }
    }

    public async Task CreateClientIfNotExists()
    {
        try
        {
            var clients = await _keycloakClient.GetClientsAsync(_keycloakOptions.Realm);
            var clientExists = clients.Any(client => client.ClientId == _keycloakOptions.ClientName);

            if (clientExists)
            {
                _logger.LogInformation("Client with name {Client} already exists.", _keycloakOptions.ClientName.ToLower());
                return;
            }
            
            await CreateClient();
        }
        catch (FlurlHttpException e) when (e.StatusCode == 404)
        {
            await CreateClient();
        }
        catch (FlurlHttpException ex)
        {
            _logger.LogCritical("Error retrieving client with id {Client}: {ErrorMessage}", _keycloakOptions.ClientName, ex.Message);
            throw new KeycloakInitializationException(ex.Message);
        }
    }

    private async Task CreateRealm()
    {
        _logger.LogInformation("Realm with name {Realm} not found. Creating...", _keycloakOptions.Realm);
        try
        {
            // Attempt to create/import the realm
            var result = await _keycloakClient.ImportRealmAsync(_keycloakOptions.Realm, PrepareRealmRepresentation());
            if (result)
            {
                _logger.LogInformation("Realm with name {Realm} has been successfully created.", _keycloakOptions.Realm);
            }
            else
            {
                _logger.LogCritical("Failed to create realm with name {Realm}.", _keycloakOptions.Realm);
                throw new KeycloakInitializationException();
            }
        }
        catch (FlurlHttpException importEx)
        {
            _logger.LogCritical("Error creating realm with name {Realm}: {ErrorMessage}", _keycloakOptions.Realm, importEx.Message);
            throw new KeycloakInitializationException(importEx.Message);
        }
    }

    private async Task CreateClient()
    {
        _logger.LogInformation("Creating client with name {Client}", _keycloakOptions.ClientName);
        try
        {
            var result = await _keycloakClient.CreateClientAsync(_keycloakOptions.Realm, PrepareClientRepresentation());
            if (result)
            {
                _logger.LogInformation("Client with name {Client} has been successfully created.", _keycloakOptions.ClientName);
            }
            else
            {
                _logger.LogCritical("Failed to create client with name {Client}.", _keycloakOptions.ClientName);
                throw new KeycloakInitializationException();
            }
        }
        catch (FlurlHttpException e)
        {
            _logger.LogCritical("Error creating client with id {Client}: {ErrorMessage}", _keycloakOptions.ClientName, e.Message);
            throw new KeycloakInitializationException(e.Message);
        }
    }

    public async Task UpdateRolesClaimMapping()
    {
        // find scope for realm roles, throw exception if null
        var rolesScope = (await _keycloakClient.GetClientScopesAsync(_keycloakOptions.Realm))
            .First(clientScope => clientScope.Name == SecurityConstants.RolesClientScope);

        var rolesMapper = rolesScope.ProtocolMappers.FirstOrDefault(mapper => mapper.Name == SecurityConstants.RolesMapper);
        if (rolesMapper is not null)
        {
            _logger.LogInformation("Roles token claim mapper in realm {Realm} already configured to {ClaimName}",
                _keycloakOptions.Realm, SecurityConstants.NewRoleClaimName);
            return;
        }

        // try to update roles client scope
        try
        {
            rolesMapper = new ProtocolMapper
            {
                Id = Guid.NewGuid().ToString(),
                Name = SecurityConstants.RolesMapper,
                ConsentRequired = false,
                Protocol = "openid-connect",
                _ProtocolMapper = "oidc-usermodel-realm-role-mapper",
                Config = new Dictionary<string, string>
                {
                    { "introspection.token.claim", "true" },
                    { "multivalued", "true" },
                    { "userinfo.token.claim", "true" },
                    { "id.token.claim", "false" },
                    { "lightweight.claim", "false" },
                    { "access.token.claim", "true" },
                    { "claim.name", "roles" },
                    { "jsonType.label", "String" }
                }
            };
            var result =
                await _keycloakClient.CreateProtocolMapperAsync(_keycloakOptions.Realm, rolesScope.Id, rolesMapper);
            if (result)
            {
                _logger.LogInformation("Roles token claim mapper in realm {Realm} configured to value {ClaimName}",
                    _keycloakOptions.Realm, SecurityConstants.NewRoleClaimName);
            }
            else
            {
                _logger.LogCritical(
                    "Roles token claim mapper in realm {Realm} configuration failed. Cannot start application.",
                    _keycloakOptions.Realm);
                throw new KeycloakInitializationException();
            }
        }
        catch (FlurlHttpException e)
        {
            _logger.LogCritical("Error updating client scope for roles token mapping: {ErrorMessage}", e.Message);
            throw new KeycloakInitializationException(e.Message);
        }
    }

    private Realm PrepareRealmRepresentation()
    {
        return new Realm
        {
            Id = _keycloakOptions.Realm,
            _Realm = _keycloakOptions.Realm,
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
            RequiredCredentials = ["password"],
            InternationalizationEnabled = true,
            DefaultLocale = "sk_SK",
            SupportedLocales = ["sk_SK"]
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
            RedirectUris = new List<string> { "http://localhost:5000/*", "http://localhost:5177/*" },
            WebOrigins = new List<string> { "http://localhost:5000", "http://localhost:5177" }
        };
    }
}