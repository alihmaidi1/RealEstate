using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Shared.Abstraction.CQRS;
public interface IQueryHandler<TCommand> where TCommand : IQuery
{
    public Task<JsonResult> Handle(TCommand request, CancellationToken cancellationToken);

}
