using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure;

public class RealEstateDbContext : DbContext
{
    public RealEstateDbContext(DbContextOptions option) : base(option)
    {


    }


    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

    public DbSet<OutBoxMessage> OutBoxMessages { get; init; }


}
