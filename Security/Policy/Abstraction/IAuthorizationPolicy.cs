using Microsoft.AspNetCore.Authorization;

namespace Security.Policy.Abstraction;

/// <summary>
/// Base interface for easier creating of authorization policies.
/// </summary>
public interface IAuthorizationPolicy
{
    AuthorizationPolicy BuildPolicy();
}