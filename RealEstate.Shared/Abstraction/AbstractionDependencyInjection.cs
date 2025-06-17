using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Shared.Abstraction.Behavior;
using RealEstate.Shared.Abstraction.CQRS;

namespace RealEstate.Shared.Abstraction;

public static class AbstractionDependencyInjection
{

    public static IServiceCollection AddAbstraction(this IServiceCollection services)
    {

        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();


        services.TryDecorate(typeof(IQueryHandler<>), typeof(ValidationDecorator.QueryHandler<>));
        
        return services;
    }

}
