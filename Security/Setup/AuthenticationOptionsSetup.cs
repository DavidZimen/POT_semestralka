using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Security.Setup;

public class AuthenticationOptionsSetup : IConfigureOptions<AuthenticationOptions>
{
    public void Configure(AuthenticationOptions o)
    {
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}