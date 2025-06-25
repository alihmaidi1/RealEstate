// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Authorization;

namespace RealEstate.Infrastructure.Authorization;

public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permission = requirement.Permission;

        var canAccess = context.User.Claims.Any(c=>c.Type=="Permission"&&c.Value==permission);
        if (canAccess)
        {
            context.Succeed(requirement);
            return;
        }
        context.Fail();
    }
}
