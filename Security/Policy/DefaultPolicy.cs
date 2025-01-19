using Microsoft.AspNetCore.Authorization;
using Security.Policy.Abstraction;

namespace Security.Policy;

/// <summary>
/// Default policy, where user just needs to be authenticates.
/// </summary>
public class DefaultPolicy : IAuthorizationPolicy
{
    public static AuthorizationPolicyBuilder DefaultPolicyBuilder() 
        => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("email_verified", bool.TrueString.ToLower());

    public AuthorizationPolicy BuildPolicy() => DefaultPolicyBuilder().Build();
}