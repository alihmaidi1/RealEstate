using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Shared.OperationResult;

public static class OperationResultExtension
{

    public static OperationResult<TEntity> SetError<TEntity>(string errorMessage)
    {

        return new OperationResult<TEntity>()
        {


            Message = errorMessage,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            Data = default(TEntity)

        };
    }

    public static OperationResult<TEntity> SetSuccess<TEntity>(TEntity data, string Message = "")
    {

        return new OperationResult<TEntity>()
        {
            Data = data,
            StatusCode = System.Net.HttpStatusCode.OK,
            Message = Message

        };
    }

    public static OperationResult<TEntity> Created<TEntity>(TEntity data, string Message = "")
    {

        return new OperationResult<TEntity>()
        {
            Data = data,
            StatusCode = System.Net.HttpStatusCode.Created,
            Message = Message

        };
    }
    public static OperationResult<TEntity> SetSuccess<TEntity>(string Message = "")
    {


        return new OperationResult<TEntity>()
        {

            StatusCode = System.Net.HttpStatusCode.OK,
            Message = Message,
            Data = default(TEntity)

        };
    }

    public static JsonResult ToJsonResult<T>(this OperationResult<T> result)
    {
        return new JsonResult(result);
    }

    
}
