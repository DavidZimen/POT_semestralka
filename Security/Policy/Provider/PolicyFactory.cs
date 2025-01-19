using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Security.Policy.Abstraction;

namespace Security.Policy.Provider;

public class PolicyFactory
{
    private readonly ILogger<PolicyFactory> _logger;

    public PolicyFactory(ILogger<PolicyFactory> logger)
    {
        _logger = logger;
    }

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

public class PolicyTuple
{
    public required string Name { get; init; }
    
    public required AuthorizationPolicy Policy { get; init; }
}