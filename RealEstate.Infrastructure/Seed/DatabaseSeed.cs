// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.Security;
using RealEstate.Infrastructure.Seed.Security;

namespace RealEstate.Infrastructure.Seed;

public static class DatabaseSeed
{


    public static async Task InitializeAsync(IServiceProvider services)
    {
    
        var context = services.GetRequiredService<RealEstateDbContext>();     
        var roleManager = services.GetRequiredService<RoleManager<Role>>();     
        var userManager = services.GetRequiredService<UserManager<User>>();     
        
        
        context.Database.EnsureCreated();    
        
        var pendingMigration = await context.Database.GetPendingMigrationsAsync();
        if (!pendingMigration.Any())
        {
            await context.Database.MigrateAsync();
            
        }
        try
        {

            await DefaultRoleSeeder.seedData(context);
            await DefaultUserSeeder.seedData(userManager);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    
    }

}
