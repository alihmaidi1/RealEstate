using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task CommitWithDomainEventAsync(CancellationToken cancellationToken)
    {
        var outBoxMessages=_context
        .ChangeTracker
        .Entries<TEntity>()
        .Select(x=>x.Entity)
        .SelectMany(entity=>{

            var DomainEvents=entity.GetDomainEvents();
            entity.ClearDomainEvents();
            return DomainEvents;
        })
        .Select(domainevent=>new OutBoxMessage{


            Content=JsonConvert.SerializeObject(domainevent,new JsonSerializerSettings{

                TypeNameHandling= TypeNameHandling.All
            })


        })
        .ToList();
        _context.Set<OutBoxMessage>().AddRange(outBoxMessages);
        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task CommitAsync(CancellationToken cancellationToken=default)
    {

        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
