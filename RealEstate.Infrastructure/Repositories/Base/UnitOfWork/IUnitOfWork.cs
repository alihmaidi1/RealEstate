using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Shared.Abstraction.Entities;

namespace RealEstate.Infrastructure.Repositories.Base.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task SaveChangesWithDomainEventAsync(CancellationToken cancellationToken);
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
