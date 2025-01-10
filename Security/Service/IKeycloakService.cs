using Security.Dto;

namespace Security.Service;

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

    /// <summary>
    /// Creates user in the 'semestralka' realm inside the keycloak.
    /// </summary>
    /// <returns>UserId, if the user was successfully created in the realm, null otherwise.</returns>
    Task<string?> CreateUserAsync(KeycloakUser user);
}