using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Contract.File.Command.UploadChunk;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Application.File.Command.UploadChunk;

public class UploadChunkHandler : ICommandHandler<UploadChunkRequest>
{
    private IDomainEventDispatcher _domainEventDispatcher;
    public UploadChunkHandler(IDomainEventDispatcher domainEventDispatcher)
    {

        _domainEventDispatcher = domainEventDispatcher;

    }
    public async Task<JsonResult?> Handle(UploadChunkRequest request, CancellationToken cancellationToken)
    {

        return new JsonResult(null);
    }
}
