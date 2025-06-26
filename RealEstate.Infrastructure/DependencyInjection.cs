using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.Security;
using RealEstate.Infrastructure.BackgroundServer;
using RealEstate.Infrastructure.BackgroundServer.Filter;
using RealEstate.Infrastructure.Repositories.Base.Security;
using RealEstate.Infrastructure.Repositories.Base.UnitOfWork;
using RealEstate.Infrastructure.Seed;

using RealEstate.Infrastructure.Services.Archive;
using RealEstate.Shared.Abstraction.Behavior;
using RealEstate.Shared.Abstraction.CQRS;

namespace RealEstate.Infrastructure;

public static class DependencyInjection
{


    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentity<User,Role>(option =>
        {
            option.SignIn.RequireConfirmedAccount = true;
            
            
        }).AddEntityFrameworkStores<RealEstateDbContext>()
            .AddApiEndpoints();
        
        
        
        services.AddTransient<IArchiveService, ArchiveService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        
        services.AddDbContext<RealEstateDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            
            option.EnableSensitiveDataLogging();

        });
        services.AddDbContext<ReadOnlyRealEstateDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("ReadOnlyDefaultConnection"));
            option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            option.EnableSensitiveDataLogging();

        });


        
        services.AddHangfire(config => 
            config.UseSqlServerStorage(configuration.GetConnectionString("HangfireDb"),new SqlServerStorageOptions()
            {
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval    = TimeSpan.FromSeconds(10),
                UseRecommendedIsolationLevel = true,
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                
                
                
            })
        );
            
        services.AddHangfireServer(options =>
        {
            options.Queues = new[] { Queues.Critical,Queues.Normal, Queues.Low };
            options.WorkerCount=5* Environment.ProcessorCount;
            options.ServerName = "Hangfire server";
            options.SchedulePollingInterval = TimeSpan.FromSeconds(10);
            

        });
        
        services.AddHangfireServer(options =>
        {
            options.Queues = new[] { Queues.Slow };
            options.WorkerCount=5;
            options.ServerName = "Hangfire server";
            options.SchedulePollingInterval = TimeSpan.FromSeconds(10);
            

        });
        services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
        
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkBehavior<>));
     
     
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
            Authorization = new []
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = "admin",
                    Pass = "password"
                }     
                
            }
        });

        return app;
    }

}
