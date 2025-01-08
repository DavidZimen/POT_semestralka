using Microsoft.AspNetCore.Http;

namespace Security.Service;

public class AuthService : IAuthService
{
    private const string UserIdClaimName = "vib";
    
    private readonly HttpContext _httpContext;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }
    
    public string? GetCurrentUserId()
    {
        return _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == UserIdClaimName)?.Value;
    }

    public bool IsUserInRole(string role)
    {
        return _httpContext.User.IsInRole(role);
    }
}