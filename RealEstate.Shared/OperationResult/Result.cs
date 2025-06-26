// Licensed to the .NET Foundation under one or more agreements.

using System.Net;

namespace RealEstate.Shared.OperationResult;

public class Result
{

    public Result(bool isSuccess, Error error,HttpStatusCode statusCode)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None,HttpStatusCode.OK);

    public static Result Failure(Error error,HttpStatusCode statusCode) => new(false, error,statusCode);

    public static TResult<TValue> Success<TValue>(TValue? value) => new(value, true, Error.None,HttpStatusCode.OK);

    // public static TResult<TValue> Failure<TValue>(Error error,HttpStatusCode statusCode) => new(default, false, error,statusCode);

    public static TResult<TValue> ValidationFailure<TValue>(Error error) => new(default, false, error,HttpStatusCode.UnprocessableContent);

    public static Result ValidationFailure(Error error) => new(false, error,HttpStatusCode.UnprocessableContent);
    public static Result InternalFailure(Error error) => new(false, error,HttpStatusCode.InternalServerError);
    
    public static TResult<TValue> InternalFailure<TValue>(Error error) => new(default, false, error,HttpStatusCode.InternalServerError);
    public static TResult<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : InternalFailure<TValue>(Error.NullValue);


}