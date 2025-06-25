// Licensed to the .NET Foundation under one or more agreements.

using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Security;
using RealEstate.Infrastructure.Services.Archive;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure;

public class ReadOnlyRealEstateDbContext: IdentityDbContext<User,Role,Guid>
{
    
    public ReadOnlyRealEstateDbContext(DbContextOptions<ReadOnlyRealEstateDbContext> option) : base(option)
    {


    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

    }

    
    [DbFunction("SOUNDEX", IsBuiltIn = true)]
    public static string FuzzySearch(string search)
    {
        
        throw new NotImplementedException();
    }
    

    
    
    public DbSet<OutBoxMessage> OutBoxMessages { get; init; }
    
    public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }

    
}
