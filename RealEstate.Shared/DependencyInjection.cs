// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Shared.Filters;
using RealEstate.Shared.Middleware;

namespace RealEstate.Shared;

public static class DependencyInjection
{

    public static IServiceCollection AddSharedLayer(this IServiceCollection services)
    {
        services.AddScoped<ApiKeyAuthFilter>();
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        return services;
    }
    
    
}
