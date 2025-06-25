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

    public static JsonResult ToJsonResult(this Result operationTResult, HttpStatusCode ResultStatusCode)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false 
        };
        return new JsonResult(operationTResult,options)
        {

            StatusCode = (int)ResultStatusCode,
                        

        };

    }


    public static async Task<JsonResult> ToJsonResultAsync(this Task<Result> operationResult, HttpStatusCode StatusCode)     {

        return (await operationResult).ToJsonResult(StatusCode);
    }
}
