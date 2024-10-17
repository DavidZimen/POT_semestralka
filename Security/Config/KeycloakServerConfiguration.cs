using Microsoft.Extensions.Hosting;
using Security.Service;

namespace Security.Config;

public class KeycloakServerConfiguration : IHostedService
{
    private readonly IKeycloakService _keycloakService;

    public KeycloakServerConfiguration(IKeycloakService keycloakService)
    {
        _keycloakService = keycloakService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _keycloakService.CreateRealmIfNotExists();
        await _keycloakService.CreateClientIfNotExists();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}