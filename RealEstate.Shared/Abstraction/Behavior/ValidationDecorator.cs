using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Shared.Abstraction.CQRS;
using RealEstate.Shared.OperationResult;

namespace RealEstate.Shared.Abstraction.Behavior;

public static class ValidationDecorator
{

    public  class CommandHandler<TCommand>(ICommandHandler<TCommand> innerHandler,
    IEnumerable<IValidator<TCommand>> validators) : ICommandHandler<TCommand>
    where TCommand : ICommand 
    {


        public async Task<IActionResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("YYYYYYYYYYYYYY");
            var validationFailures = await ValidateAsync(request, validators);
            if (validationFailures == null)
            {
                return await innerHandler.Handle(request, cancellationToken);
            }
            return Result.ValidationFailure(Error.ValidationFailures(validationFailures)).ToJsonResult();

        }
    }


    public sealed class QueryHandler<TQuery>(IQueryHandler<TQuery> innerHandler,
    IEnumerable<IValidator<TQuery>> validators) : IQueryHandler<TQuery>
    where TQuery : IQuery 
    {
        public async Task<IActionResult> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var validationFailures = await ValidateAsync(request, validators);
            if (validationFailures == null)
            {
                return await innerHandler.Handle(request, cancellationToken);
            }
            return  Result.ValidationFailure(Error.ValidationFailures(validationFailures)).ToJsonResult();

        }
    }









    private static async Task<string?> ValidateAsync<TCommand>(TCommand request, IEnumerable<IValidator<TCommand>> validators)
    {

        if (!validators.Any())
        {

            return null;
        }
        var context = new ValidationContext<TCommand>(request);

        ValidationResult[] validationResult = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context)));
        var validationFailures = validationResult
        .Where(validationResult => !validationResult.IsValid)
        .SelectMany(validationResult => validationResult.Errors)
        .ToList();

        if (!validationFailures.Any())
        {

            return null;
        }
        else
        {

            return validationFailures.First().ErrorMessage;
        }

    }




}
