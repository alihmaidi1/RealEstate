using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Shared.Abstraction.CQRS;

public interface ICommandHandler<TCommand> where TCommand: ICommand
{
    public Task<JsonResult> Handle(TCommand request, CancellationToken cancellationToken);
    
}
