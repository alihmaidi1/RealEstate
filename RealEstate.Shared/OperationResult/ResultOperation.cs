using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Shared.OperationResult;

public static class ResultOperation
{

    public static JsonResult ToJsonResult<T>(this Result<T> operationResult, HttpStatusCode ResultStatusCode)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false 
        };
        return new JsonResult(operationResult.Data,options)
        {

            StatusCode = (int)ResultStatusCode,
                        

        };

    }


    public static async Task<JsonResult> ToJsonResultAsync<T>(this Task<Result<T>> operationResult, HttpStatusCode StatusCode) where T : class
    {

        return (await operationResult).ToJsonResult(StatusCode);
    }
}
