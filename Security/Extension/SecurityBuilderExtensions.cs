﻿using Keycloak.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Security.Config;
using Security.Options;
using Security.Policy.Provider;
using Security.Service;

namespace Security.Extension;

public static class SecurityBuilderExtensions
{
    private const string KeycloakOptionsName = "Keycloak";
    private const string NameClaimType = "preferred_username";
    
    public static IHostApplicationBuilder ConfigureKeycloakForApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOptions<KeycloakOwnOptions>().BindConfiguration(KeycloakOptionsName);
        
        builder.Services.AddKeycloakServices();
        
        return builder;
    }

    public static IHostApplicationBuilder ConfigureKeycloakServer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddKeycloakServices();
        builder.Services.AddHostedService<KeycloakServerConfiguration>();
        return builder;
    }

    public static IHostApplicationBuilder AddKeycloakToApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                var keycloakSection = builder.Configuration.GetRequiredSection(KeycloakOptionsName);
                o.RequireHttpsMetadata = false;
                o.Audience = keycloakSection["Audience"];
                o.MetadataAddress = keycloakSection["MetadataAddress"] ?? throw new InvalidOperationException();

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = keycloakSection["ValidIssuer"],
                    NameClaimType = NameClaimType
                };
            });

        // add own provider for policies and authorization
        builder.AddAuthorizationWithPolicies();
        
        return builder;
    }

    public static WebAssemblyHostBuilder AddKeycloakToClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        
        builder.Services.AddOidcAuthentication(o =>
        {
            var keycloakSection = builder.Configuration.GetRequiredSection(KeycloakOptionsName);
            
            o.ProviderOptions.MetadataUrl = keycloakSection["MetadataAddress"] ?? throw new InvalidOperationException();
            o.ProviderOptions.ClientId = keycloakSection["ClientName"];
            o.ProviderOptions.Authority = keycloakSection["ValidIssuer"];
            o.ProviderOptions.ResponseType = "id_token token";
            o.ProviderOptions.DefaultScopes.Add("openid");
            o.ProviderOptions.DefaultScopes.Add("profile");
            o.ProviderOptions.DefaultScopes.Add("email");

            o.UserOptions.NameClaim = NameClaimType;
            o.UserOptions.ScopeClaim = "scope";
            o.UserOptions.RoleClaim = "role";
        });

        return builder;
    }

    public static IHostApplicationBuilder AddSwaggerGenWithAuth(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(id => id.FullName!.Replace("+", "-"));

            var securityDefinition = new OpenApiSecurityScheme 
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration["Keycloak:AuthorizationUrl"] ?? throw new InvalidOperationException()),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" },
                            { "profile", "profile" },
                            { "email", "email" }
                        }
                    }
                }
            };
            o.AddSecurityDefinition(KeycloakOptionsName,securityDefinition);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = KeycloakOptionsName,
                            Type = ReferenceType.SecurityScheme
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = JwtBearerDefaults.AuthenticationScheme
                    },
                    []
                }
            };
            o.AddSecurityRequirement(securityRequirement);
        });
        return builder;
    }
    
    private static void AddAuthorizationWithPolicies(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<PolicyFactory>();
        builder.Services.AddSingleton<IAuthorizationPolicyProvider, PolicyProvider>();
        builder.Services.AddAuthorization();
    }

    private static void AddKeycloakServices(this IServiceCollection services)
    {
        if (!services.IsServiceRegistered<KeycloakClient>())
        {
            services.AddSingleton<KeycloakClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<KeycloakOwnOptions>>().Value;
                ArgumentNullException.ThrowIfNull(options);
                return new KeycloakClient(options.Url, options.Username, options.Password, new KeycloakOptions(authenticationRealm:"master"));
            });
        }

        if (!services.IsServiceRegistered<IKeycloakService, KeycloakService>())
        {
            services.AddSingleton<IKeycloakService, KeycloakService>();
        }
    }
}