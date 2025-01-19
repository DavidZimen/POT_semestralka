using Microsoft.AspNetCore.Authorization;
using Security.Enums;
using Security.Policy.Abstraction;

namespace Security.Policy;

/// <summary>
/// Policy for access only for admin.
/// </summary>
public class AdminPolicy : IAuthorizationPolicy
{
    public AuthorizationPolicy BuildPolicy() 
        => DefaultPolicy.DefaultPolicyBuilder()
            .RequireRole(Role.Admin)
            .Build();
}