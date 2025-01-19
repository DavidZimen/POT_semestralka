using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Security.Policy.Abstraction;

namespace Security.Policy.Provider;

/// <summary>
/// Factory for easies creating of authorization policies.
/// </summary>
public class PolicyFactory
{
    private readonly ILogger<PolicyFactory> _logger;

    public PolicyFactory(ILogger<PolicyFactory> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Create new Policy based on provided type.
    /// </summary>
    /// <param name="policyType">Type of policy to be created.</param>
    /// <returns>Tuple of policy name and its instance.</returns>
    public PolicyTuple? GetPolicy(Type policyType)
    {
        if (!typeof(IAuthorizationPolicy).IsAssignableFrom(policyType))
        {
            _logger.LogError("Type {PolicyType} does not implement IAuthorizationPolicy.", policyType);
            return null;
        }

        IAuthorizationPolicy? policy = null;
        try
        {
            policy = Activator.CreateInstance(policyType) as IAuthorizationPolicy;
        }
        catch (System.Exception e)
        {
            _logger.LogError("Failed to crate policy of type {PolicyType}: {ErrorMessage}", policyType, e.Message);
        }

        return policy == null ? null : new PolicyTuple { Name = policyType.Name, Policy = policy.BuildPolicy() };
    }
}

/// <summary>
/// Tuple for easier registering of policies into the authorization middleware.
/// </summary>
public class PolicyTuple
{
    public required string Name { get; init; }
    
    public required AuthorizationPolicy Policy { get; init; }
}