﻿using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Security.Policy.Abstraction;

namespace Security.Policy.Provider;

public class PolicyProvider : IAuthorizationPolicyProvider
{
    /// <summary>
    /// Namespace/directory, where all the possible policies for application are defined.
    /// All classes that inherit <b>IAuthorizationPolicy</b> will be added to the <b>PolicyProvider</b>.
    /// </summary>
    private const string PoliciesNamespace = "Security.Policy";

    /// <summary>
    /// Factory, where all the policies will be created.
    /// </summary>
    private readonly PolicyFactory _policyFactory;
    
    /// <summary>
    /// Storage for policies, so there will not be created new instances with each call to get policy.
    /// Key is the name of the policy class.
    /// </summary>
    private readonly Dictionary<string, AuthorizationPolicy> _authorizationPolicies;

    public PolicyProvider(PolicyFactory policyFactory, ILogger<PolicyProvider> logger)
    {
        _policyFactory = policyFactory;
        _authorizationPolicies = InitializePolicies();
        logger.LogInformation("Policies loaded: {Count}, Names: {Names}", _authorizationPolicies.Count, string.Join(", ", _authorizationPolicies.Keys));
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName) 
        => _authorizationPolicies.TryGetValue(policyName, out var policy) ? 
            Task.FromResult<AuthorizationPolicy?>(policy) : Task.FromResult<AuthorizationPolicy?>(null);

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(_authorizationPolicies[nameof(DefaultPolicy)]);

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(null);

    /// <summary>
    /// Initializes all policies, that are in given namespace with type <seealso cref="IAuthorizationPolicy"/>
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, AuthorizationPolicy> InitializePolicies() 
        => typeof(IAuthorizationPolicy).Assembly.GetTypes()
            .Where(type => type.Namespace == PoliciesNamespace)
            .Where(type => type.IsClass)
            .Select(_policyFactory.GetPolicy)
            .Where(policyTuple => policyTuple is not null)
            .ToDictionary(policyTuple => policyTuple!.Name, policyTuple => policyTuple!.Policy);
}