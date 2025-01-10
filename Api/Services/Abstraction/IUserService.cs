using Domain.Dto;

namespace Api.Services.Abstraction;

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