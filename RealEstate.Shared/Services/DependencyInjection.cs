using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Shared.Services.Sms;

namespace RealEstate.Shared.Services;

public static class DependencyInjection
{

    
    public static IServiceCollection AddSharedServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<TwilioSmsSetting>(configuration.GetSection("Twilio"));
        services.AddTransient<ISmsTwilioService,SmsTwilioService>();
        return services;
    }
    
}
