using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

RegisterHttpClient(builder);

builder.Services.AddOidcAuthentication(o =>
{
    var keycloakSection = builder.Configuration.GetSection("Keycloak");
            
    o.ProviderOptions.MetadataUrl = keycloakSection["MetadataAddress"] ?? throw new InvalidOperationException();
    o.ProviderOptions.ClientId = keycloakSection["ClientName"];
    o.ProviderOptions.Authority = keycloakSection["ValidIssuer"];
    o.ProviderOptions.ResponseType = "id_token token";
    o.ProviderOptions.DefaultScopes.Add("openid");
    o.ProviderOptions.DefaultScopes.Add("profile");
    o.ProviderOptions.DefaultScopes.Add("email");

    o.UserOptions.NameClaim = "preferred_username";
    o.UserOptions.ScopeClaim = "scope";
    o.UserOptions.RoleClaim = "role";
});

await builder.Build().RunAsync();

static void RegisterHttpClient(
    WebAssemblyHostBuilder builder)
{
    var httpClientName = "Default";
    Console.WriteLine(builder.HostEnvironment.BaseAddress);
    
    builder.Services.AddHttpClient(httpClientName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

    builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName));
}