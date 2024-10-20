using Microsoft.AspNetCore.Authorization;
using Security.Policy.Abstraction;

namespace Security.Policy;

public class DefaultPolicy : IAuthorizationPolicy
{
    public static AuthorizationPolicyBuilder DefaultPolicyBuilder() 
        => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("email_verified", bool.TrueString.ToLower());

    public AuthorizationPolicy BuildPolicy() => DefaultPolicyBuilder().Build();
}