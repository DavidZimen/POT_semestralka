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

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _keycloakService.CreateRealmIfNotExists();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}