// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstate.Infrastructure.Seed;

public static class DatabaseSeed
{


    public static async Task InitializeAsync(IServiceProvider services)
    {
    
        var context = services.GetRequiredService<RealEstateDbContext>();        
        context.Database.EnsureCreated();    
        var pendingMigration = await context.Database.GetPendingMigrationsAsync();
        if (!pendingMigration.Any())
        {
            await context.Database.MigrateAsync();
            
        }
        try
        {
            
        
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    
    }

}
