using Microsoft.AspNetCore.Authorization;

namespace Security.Policy.Provider;

public class PolicyProvider : IAuthorizationPolicyProvider
{

    private readonly PolicyFactory _policyFactory;
    
    private readonly Dictionary<string, AuthorizationPolicy> _authorizationPolicies = new();

    public PolicyProvider(PolicyFactory policyFactory)
    {
        _policyFactory = policyFactory;
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        throw new NotImplementedException();
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        throw new NotImplementedException();
    }
}