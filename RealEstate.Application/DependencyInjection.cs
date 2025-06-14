using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Shared.Abstraction;

namespace RealEstate.Application;

public static class DependencyInjection
{


    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAbstraction();
        services.Configure<ApiBehaviorOptions>(options =>
        {

            options.SuppressModelStateInvalidFilter = true;

        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
    