﻿using Microsoft.Extensions.Hosting;
using Security.Service;

namespace Security.Config;

/// <summary>
/// Hosted service for configuring running Keycloak instance to the correct state.
/// </summary>
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
        await _keycloakService.UpdateRolesClaimMapping();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}