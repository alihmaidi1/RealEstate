// Licensed to the .NET Foundation under one or more agreements.


using Hangfire;
using Newtonsoft.Json;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure.BackgroundServer;

public class ProcessOutboxMessageJob
{
    private readonly RealEstateDbContext _dbContext;
    private readonly IDomainEventDispatcher  _domainEventDispatcher;

    public ProcessOutboxMessageJob(RealEstateDbContext context, IDomainEventDispatcher  domainEventDispatcher)
    {
        
        _dbContext = context;
        _domainEventDispatcher = domainEventDispatcher;
        
    }

    public void Run(CancellationToken token)
    {


        var messages = _dbContext
                
            .Set<OutBoxMessage>()
            .ToList();
        foreach (var message in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content);
            if (domainEvent is null)
            {
                continue;
                
            }

            // _domainEventDispatcher.DispatchAsync(domainEvent,token);
        }
    }
    
}
