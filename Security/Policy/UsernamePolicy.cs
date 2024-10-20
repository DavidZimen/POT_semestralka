using Microsoft.AspNetCore.Authorization;
using Security.Policy.Abstraction;

namespace Security.Policy;

public class UsernamePolicy : IAuthorizationPolicy
{
    public AuthorizationPolicy BuildPolicy()
        => DefaultPolicy
            .DefaultPolicyBuilder()
            .RequireUserName("david.zimen1@gmail.com")
            .Build();
}