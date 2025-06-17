using System.Reflection;
using Microsoft.EntityFrameworkCore;

using RealEstate.Infrastructure.Services.Archive;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure;

public class RealEstateDbContext : DbContext
{
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> option) : base(option)
    {


    }


    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

    public DbSet<OutBoxMessage> OutBoxMessages { get; init; }
    
    public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }

    

}
