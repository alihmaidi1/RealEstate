// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.EntityFrameworkCore;
using RealEstate.Infrastructure.Services.Archive;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure;

public class ReadOnlyRealEstateDbContext: DbContext
{
    
    public ReadOnlyRealEstateDbContext(DbContextOptions<ReadOnlyRealEstateDbContext> option) : base(option)
    {


    }
    [DbFunction("SOUNDEX", IsBuiltIn = true)]
    public static string FuzzySearch(string search)
    {
        
        throw new NotImplementedException();
    }
    
    
    public DbSet<OutBoxMessage> OutBoxMessages { get; init; }
    
    public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }

    
}
