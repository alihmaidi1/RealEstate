using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RealEstate.Contract.File.Command.UploadChunk;

namespace RealEstate.Application.File.Command.UploadChunk;

public class UploadChunkValidation : AbstractValidator<UploadChunkRequest>
{
    public UploadChunkValidation()
    {

        RuleFor(x => x.partNumber)
        .NotNull()
        .NotEmpty();

        RuleFor(x => x.key)
        .NotEmpty()
        .NotNull();

        RuleFor(x => x.uploadId)
        .NotEmpty()
        .NotNull();

    }
   
    
}
