using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Contract.File.Command.UploadChunk;
using RealEstate.Shared.Abstraction.CQRS;

namespace RealEstate.Api.Controllers;
[Route("[controller]/[action]")]
[ApiController]
public class FileController
{
    // [HttpPost]
    //
    // public async Task<JsonResult> UploadChunk([FromBody] UploadChunkRequest uploadChunkRequest, [FromServices] ICommandHandler<UploadChunkRequest> commandHandler, CancellationToken cancellationToken)
    // {
    //     return await commandHandler.Handle(uploadChunkRequest, cancellationToken);
    //
    // }


}
