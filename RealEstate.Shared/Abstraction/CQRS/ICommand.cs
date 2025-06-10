using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Shared.Abstraction.CQRS;

public interface ICommand;

public interface ICommand<TResponse> where TResponse : OperationResult.Result<TResponse>;
