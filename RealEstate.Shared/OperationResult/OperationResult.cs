using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RealEstate.Shared.OperationResult;


public class OperationResult<TEntity>
{

    public OperationResult(HttpStatusCode status = HttpStatusCode.OK)
    {
        StatusCode = status;
    }

    public TEntity? Data { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }


    public List<ValidationError> Errors { get; set; }



}


public class ValidationError
{
    public string Field { get; set; }
    public string Message { get; set; }
}