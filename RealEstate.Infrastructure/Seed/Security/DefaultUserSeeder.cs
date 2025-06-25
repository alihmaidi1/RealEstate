// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Security;

namespace RealEstate.Infrastructure.Seed.Security;

public static class DefaultUserSeeder
{


    public static async Task seedData(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var defaultUser = new User()
            {
                UserName = nameof(StaticRole.SuperAdmin),
                Email = nameof(StaticRole.SuperAdmin)+"@gmail.com",
                EmailConfirmed = true,
                UserType = UserType.Admin,
                
                
                
            };
            await userManager.CreateAsync(defaultUser, "StrongPassword123!");
            await userManager.AddToRoleAsync(defaultUser, nameof(StaticRole.SuperAdmin));
        }
        
    }
    
}
