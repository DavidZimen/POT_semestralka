using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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

public class AuthService : IAuthService
{
    private const string UserIdClaimName = "sub";
    
    private readonly HttpContext _httpContext;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }
    
    public string? GetCurrentUserId()
    {
        return _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public bool IsUserInRole(string role)
    {
        return _httpContext.User.IsInRole(role);
    }
}