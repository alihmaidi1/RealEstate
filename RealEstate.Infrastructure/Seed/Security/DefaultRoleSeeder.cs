// Licensed to the .NET Foundation under one or more agreements.

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Security;

namespace RealEstate.Infrastructure.Seed.Security;

public static class DefaultRoleSeeder
{

    public static async Task seedData(RealEstateDbContext dbContext)
    {
        if (!await dbContext.Roles.AnyAsync())
        {
            dbContext.Roles.AddRange(Enum.GetNames(typeof(StaticRole)).Select(x=>new Role()
            {
                
                Name = x,
                NormalizedName = x.ToUpper()
                
            }).ToList());
            await dbContext.SaveChangesAsync();
            var superadminRole = await dbContext.Roles.FirstAsync(x=>x.Name==nameof(StaticRole.SuperAdmin))!;
            var permissions = Enum.GetNames(typeof(Permissions)).Select(x=>new IdentityRoleClaim<Guid>
            {
                ClaimType    = "Permission",
                ClaimValue = x,
                RoleId      = superadminRole.Id,
                
            }).ToList();
            dbContext.RoleClaims.AddRange(permissions);
            await dbContext.SaveChangesAsync();
        }



    }


}
