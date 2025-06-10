using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.OperationResult;

public class Result<T>
{


    public T Data { get; set; }

    public string Message { get; set; }

    public bool IsSuccess { get; set; } = true;


    public static Result<T> SetSuccess(string Message = "")
    {


        return new Result<T>
        {

            Message = Message
        };
    }


    public static Result<T> SetSuccess(T Data)
    {


        return new Result<T>
        {
            Data = Data
        };
    }


    public static Result<T> SetSuccess(T Data, string Message)
    {


        return new Result<T>
        {
            Data = Data,
            Message = Message
        };
    }


    public static Result<T> SetError(string Message)
    {

        return new Result<T>
        {

            IsSuccess = false,
            Message=Message
            
        };
    }

}







