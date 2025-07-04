﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using RealEstate.Shared.Abstraction.Entities;
using RealEstate.Shared.Abstraction.Entities.Entity;

namespace RealEstate.Infrastructure.Repositories.Base.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly RealEstateDbContext _context;

    public UnitOfWork(RealEstateDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesWithDomainEventAsync(CancellationToken cancellationToken)
    {
        var outBoxMessages = _context
        .ChangeTracker
        .Entries<TEntity>()
        .Select(x => x.Entity)
        .SelectMany(entity =>
        {

            var DomainEvents = entity.GetDomainEvents();
            entity.ClearDomainEvents();
            return DomainEvents;
        })
        .Select(domainevent => new OutBoxMessage
        {


            Content = JsonConvert.SerializeObject(domainevent, new JsonSerializerSettings
            {

                TypeNameHandling = TypeNameHandling.All
            })


        })
        .ToList();
        _context.Set<OutBoxMessage>().AddRange(outBoxMessages);
        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = _context.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
