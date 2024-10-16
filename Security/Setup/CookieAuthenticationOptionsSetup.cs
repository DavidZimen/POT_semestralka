using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace Security.Setup;

public class CookieAuthenticationOptionsSetup : IConfigureOptions<CookieAuthenticationOptions>
{
    public static readonly string AuthCookieName = "access_token";
    
    public void Configure(CookieAuthenticationOptions o)
    {
        o.Cookie.Name = AuthCookieName;
        o.Events.OnValidatePrincipal = context =>
        {
            var token = context.Request.Cookies[AuthCookieName];
            if (!string.IsNullOrWhiteSpace(token)) return Task.CompletedTask;
            context.RejectPrincipal();
            return Task.CompletedTask;

        };
    }
}