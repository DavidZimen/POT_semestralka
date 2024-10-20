using Microsoft.AspNetCore.Authorization;
using Security.Enums;
using Security.Policy.Abstraction;

namespace Security.Policy;

public class TesterPolicy : IAuthorizationPolicy
{
    public AuthorizationPolicy BuildPolicy()
        => DefaultPolicy
            .DefaultPolicyBuilder()
            .RequireRole(Role.Test, Role.Admin)
            .Build();
}