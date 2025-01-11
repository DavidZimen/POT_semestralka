using Microsoft.AspNetCore.Authorization;
using Security.Enums;
using Security.Policy.Abstraction;

namespace Security.Policy;

public class AdminPolicy : IAuthorizationPolicy
{
    public AuthorizationPolicy BuildPolicy() 
        => DefaultPolicy.DefaultPolicyBuilder()
            .RequireRole(Role.Admin)
            .Build();
}