// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using RealEstate.Domain.Security;

namespace RealEstate.Infrastructure.Authorization;

public class PermissionProvider: IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _defaultAuthorizationPolicyProvider;
    
    public PermissionProvider(IOptions<AuthorizationOptions> options) {
        
        _defaultAuthorizationPolicyProvider=new DefaultAuthorizationPolicyProvider(options); 
    }


    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var permissions = Enum.GetNames(typeof(Permissions)).ToList();
        if (permissions.Contains(policyName))
        {
            
            var policy = new AuthorizationPolicyBuilder();
        
            policy.AddRequirements(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build());
                
        }
        return this._defaultAuthorizationPolicyProvider.GetDefaultPolicyAsync();
        

    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        
        return this._defaultAuthorizationPolicyProvider.GetDefaultPolicyAsync();
        
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return this._defaultAuthorizationPolicyProvider.GetFallbackPolicyAsync();
        
    }
}
