using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.Abstraction.CQRS;
public interface IDomainEvent;

public interface IDomainEventHandler<T> where T : IDomainEvent
{

    public Task Handle(T domainEvent,CancellationToken cancellationToken);

}


