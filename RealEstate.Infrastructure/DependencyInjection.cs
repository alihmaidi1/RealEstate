using Hangfire;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Infrastructure.Seed;
using RealEstate.Infrastructure.Services.Archive;

namespace RealEstate.Infrastructure;

public static class DependencyInjection
{


    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddTransient<IArchiveService, ArchiveService>();

        services.AddDbContext<RealEstateDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            option.EnableSensitiveDataLogging();

        });
        

        services.AddHangfire(config => config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"))
        .UseDashboardMetrics()
        
        );
        services.AddHangfireServer(options =>
        {
            options.Queues = new[] { "critical" };
            options.WorkerCount = 5;
            options.ServerName = "Critical-Server";
        });
        
        services.AddHangfireServer(options =>
        {
            options.Queues = new[] { "default" };
            options.WorkerCount = 10;
            options.ServerName = "Default-Server";
        });


        return services;
    }


    public static async Task<WebApplication> UseInfrastructure(this WebApplication app)
    {
        
        using(var scope= app.Services.CreateScope()){
    
            await DatabaseSeed.InitializeAsync(scope.ServiceProvider);
        }
        app.UseHangfireDashboard("/jobs", new DashboardOptions {
            DashboardTitle = "Background Server",
            StatsPollingInterval = 5000,
        });

        return app;
    }

}
