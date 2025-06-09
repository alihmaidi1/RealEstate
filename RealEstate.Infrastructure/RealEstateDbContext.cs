using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure;

public class RealEstateDbContext : DbContext
{


    protected override void OnModelCreating(ModelBuilder builder)
    { 

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
    
}
