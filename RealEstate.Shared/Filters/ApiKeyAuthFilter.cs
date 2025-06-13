// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using RealEstate.Shared.Constant;

namespace RealEstate.Shared.Filters;

public class ApiKeyAuthFilter: IAuthorizationFilter
{
    
    private readonly IConfiguration  _configuration;

    public ApiKeyAuthFilter(IConfiguration  configuration)
    {
        _configuration=configuration;
        
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var apiKey))
        {
         
            context.Result=new UnauthorizedObjectResult("Missing API Key");
            return; 
            
        }

        var ExistsApiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
        if (!ExistsApiKey.Equals(apiKey))
        {
            context.Result=new UnauthorizedObjectResult("Invalid Api Key");
            
        }

    }
}
