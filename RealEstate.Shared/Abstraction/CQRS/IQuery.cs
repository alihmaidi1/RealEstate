using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.Abstraction.CQRS;

public interface IQuery;
public interface IQuery<TResponse> where TResponse : OperationResult.Result<TResponse>;
