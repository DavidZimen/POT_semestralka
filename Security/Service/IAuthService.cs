using Security.Enums;

namespace Security.Service;

/// <summary>
/// Service for operation on current users and getting them from HttpContext.
/// </summary>
public interface IAuthService
{
    /// <returns>ID of currently authenticated user, or null if no user is authenticated.</returns>
    string? GetCurrentUserId();

    /// <summary>
    /// Determines if the user has assigned the role or has role that is above the hierarchy.
    /// </summary>
    /// <param name="role">Role to be checked.</param>
    bool IsUserInRole(string role);
}