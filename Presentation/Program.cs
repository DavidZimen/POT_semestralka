using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Presentation;
using Security.Client.Extensions;
using Security.Client.Handler;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

RegisterHttpClient(builder);

builder.AddKeycloakToClient();

await builder.Build().RunAsync();

static void RegisterHttpClient(
    WebAssemblyHostBuilder builder)
{
    var httpClientName = "Default";

    builder.Services.AddTransient<ApiAuthorizationMessageHandler>();
    
    builder.Services.AddHttpClient(httpClientName, 
            client => client.BaseAddress = new Uri(builder.Configuration["ApiUrl"] ?? throw new InvalidOperationException("ApiUrl not configured!")))
        .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

    builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName));
}