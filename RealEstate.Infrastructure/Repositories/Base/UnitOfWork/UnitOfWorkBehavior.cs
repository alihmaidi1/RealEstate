// Licensed to the .NET Foundation under one or more agreements.

using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Infrastructure.Repositories.Base.UnitOfWork;

public class UnitOfWorkBehavior<TCommand>(IUnitOfWork _unitOfWork,ICommandHandler<TCommand> innerHandler) : ICommandHandler<TCommand>
    where TCommand : ICommand  
{
    
    

    public async Task<IActionResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        using (var transactionScope=new TransactionScope())
        {
            
            var result = await innerHandler.Handle(request, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transactionScope.Complete();
            return result;
            
            
        }
    }

}
