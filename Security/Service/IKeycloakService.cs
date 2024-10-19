namespace Security.Service;

public interface IKeycloakService
{
    /// <summary>
    /// Creates and configures new realm for storing information about users for this application.
    /// </summary>
    Task CreateRealmIfNotExists();

    /// <summary>
    /// Creates and configures client inside Keycloak realm, that will be responsible for issuing
    /// JWT tokens for authorization.
    /// </summary>
    Task CreateClientIfNotExists();

    /// <summary>
    /// Updates mapping of roles claim in JWT token from realm_access.roles to roles.
    /// </summary>
    Task UpdateRolesClaimMapping();
}