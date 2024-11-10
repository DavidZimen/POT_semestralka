using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Presentation;
using Security.Extension;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// load shared configuration files
var environment = builder.HostEnvironment.IsDevelopment() ? ".Development" : string.Empty;

using var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<Dictionary<string, string>>($"appsettings{environment}.json");

if (config != null)
{
    builder.Configuration.AddInMemoryCollection(config!);
}

RegisterHttpClient(builder);

builder.AddKeycloakToClient();

await builder.Build().RunAsync();

static void RegisterHttpClient(
    WebAssemblyHostBuilder builder)
{
    var httpClientName = "Default";
    
    builder.Services.AddHttpClient(httpClientName, client => client.BaseAddress = new Uri("http://localhost:5177/"))
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

    builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName));
}