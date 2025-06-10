using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Shared.Abstraction.CQRS;

namespace RealEstate.Shared.Abstraction.Entities;

public class TEntity : IEntity
{

    public Guid Id { get; set; }
    private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    
}
