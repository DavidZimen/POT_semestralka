namespace Security.Exception;

/// <summary>
/// Shows, that something went wrong during the initializing Keycloak server,
/// so the application is not ready to be started.
/// </summary>
/// <param name="message">Error message due to which this exception is thrown.</param>
public class KeycloakInitializationException(string? message = default) : SystemException(message);