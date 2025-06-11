using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Shared.Abstraction.CQRS;

namespace RealEstate.Contract.File.Command.UploadChunk;

public class UploadChunkRequest : ICommand
{

    public string uploadId { get; set; }

    public int partNumber { get; set; }

    public string key { get; set; }

}
