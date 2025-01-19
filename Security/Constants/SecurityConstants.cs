namespace Security.Constants;

/// <summary>
/// Constants for security configuration.
/// </summary>
public static class SecurityConstants
{
    public const string KeycloakOptionsName = "Keycloak";
    public const string UsernameClaim = "preferred_username";
    public const string RolesClientScope = "roles";
    public const string RolesMapper = "realm roles asp.net";
    public const string NewRoleClaimName = "roles";
}