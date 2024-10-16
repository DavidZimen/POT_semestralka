using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Security.Setup;

public class AuthorizationOptionsSetup : IConfigureOptions<AuthorizationOptions>
{
    private const string EmailVerifiedPolicy = "email_verified";
    
    public void Configure(AuthorizationOptions o)
    {
        o.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(EmailVerifiedPolicy, bool.TrueString)
            .Build();
    }
}