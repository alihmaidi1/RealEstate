using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.OperationResult;

public class TResult<TValue>: Result
{
    private readonly TValue? _value;

    public TResult(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value { get; }
    public static implicit operator TResult<TValue>(TValue? value) => Create(value);



    
    

}







