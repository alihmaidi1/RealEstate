using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure.Repositories.Base;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
    
    Task CommitWithDomainEventAsync(CancellationToken cancellationToken);

}
