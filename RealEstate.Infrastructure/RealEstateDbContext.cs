using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Security;
using RealEstate.Infrastructure.Services.Archive;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure;

public class RealEstateDbContext : IdentityDbContext<User,Role,Guid>
{
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> option) : base(option)
    {


    }


    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

    }

    public DbSet<OutBoxMessage> OutBoxMessages { get; init; }
    
    public DbSet<ArchiveRecord>  ArchiveRecords { get; init; }

    

}
