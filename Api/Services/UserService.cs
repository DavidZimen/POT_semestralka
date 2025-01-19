using Api.Services.Abstraction;
using Domain.Dto;
using Domain.Entity;
using Persistence.Repositories;

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
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<bool> Register(RegisterUser user)
    {
        var userEntity = await _userRepository.FindByIdAsync(user.Id);
        if (userEntity is not null)
        {
            _logger.LogInformation("User with id {ID} already exists.", user.Id);
            return true;
        }
        
        userEntity = new UserEntity
        {
            Id = user.Id,
            Enabled = true
        };

        return await _userRepository.CreateAsync(userEntity) is not null;
    }
}