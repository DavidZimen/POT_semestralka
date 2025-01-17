using Api.Services.Abstraction;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;
using Security.Dto;
using Security.Service;

namespace Api.Services;

/// <summary>
/// Service for operation with application users.
/// </summary>
public interface IUserService : IService
{
    /// <summary>
    /// Registers new user into the application
    /// </summary>
    /// <param name="user">User to be registered.</param>
    /// <returns>True, if user was registered successfully.</returns>
    Task<bool> Register(RegisterUser user);
}

public class UserService : IUserService
{
    /// <summary>
    /// Service to communicate with Keycloak identity server..
    /// </summary>
    private readonly IKeycloakService _keycloakService;

    private readonly IUserRepository _userRepository;

    public UserService(IKeycloakService keycloakService, IUserRepository userRepository)
    {
        _keycloakService = keycloakService;
        _userRepository = userRepository;
    }

    public async Task<bool> Register(RegisterUser user)
    {
        var keycloakId = await _keycloakService.CreateUserAsync(new KeycloakUser
        (
            user.Email,
            user.Password,
            user.FirstName, user.LastName,
            user.Role
        ));

        // user was not saved to keycloak
        if (keycloakId is null)
        {
            return false;
        }

        var userEntity = new UserEntity
        {
            Id = keycloakId,
            Enabled = true
        };

        return await _userRepository.CreateAsync(userEntity) is not null;
    }
}