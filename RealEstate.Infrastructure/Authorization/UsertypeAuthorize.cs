// Licensed to the .NET Foundation under one or more agreements.

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Domain.Security;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Infrastructure.Authorization;

public class UsertypeAuthorize:AuthorizeAttribute, IAuthorizationFilter
{
    private UserType _userType;
    public UsertypeAuthorize(UserType userType)
    {
        _userType=userType;
        
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userType=context.HttpContext.User.Claims.FirstOrDefault(c => c.Type=="UserType")?.Value;
        if (userType!=_userType.ToString())
        {
            context.Result = Result.Failure(Error.InvalidUserType).ToJsonResult(HttpStatusCode.Forbidden);

        }
    }
}
