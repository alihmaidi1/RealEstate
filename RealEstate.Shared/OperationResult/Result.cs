// Licensed to the .NET Foundation under one or more agreements.

namespace RealEstate.Shared.OperationResult;

public class Result
{

    public Result(bool isSuccess, Error error)
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
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static TResult<TValue> Success<TValue>(TValue? value) => new(value, true, Error.None);

    public static TResult<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static TResult<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);


}