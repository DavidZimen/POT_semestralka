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
            _logger.LogError($"Type {policyType} does not implement IAuthorizationPolicy.");
            return null;
        }

        IAuthorizationPolicy? policy = null;
        try
        {
            policy = Activator.CreateInstance(policyType) as IAuthorizationPolicy;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to crate policy of type {policyType}: {e.Message}");
        }

        return policy == null ? null : new PolicyTuple { Name = policyType.Name, Policy = policy.BuildPolicy() };
    }
}

public class PolicyTuple
{
    public string Name { get; init; }
    
    public AuthorizationPolicy Policy { get; init; }
}