using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Shared.OperationResult;

public static class ResultOperation
{

    public static JsonResult ToJsonResult<T>(this Result<T> operationResult, HttpStatusCode ResultStatusCode)
    {
        // using var operationResultBase=CreateOperationResultBase<T>(result,message,statusCode);
        return new JsonResult(operationResult)
        {

            StatusCode = (int)ResultStatusCode,


        };

    }


    public static async Task<JsonResult> ToJsonResultAsync<T>(this Task<Result<T>> operationResult, HttpStatusCode StatusCode) where T : class
    {

        return (await operationResult).ToJsonResult(StatusCode);
    }
}
