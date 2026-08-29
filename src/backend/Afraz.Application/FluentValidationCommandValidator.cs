using FluentValidation;
using Infra.Commands;

namespace Afraz.Application;

public sealed class FluentValidationCommandValidator<TCommand>(
    IEnumerable<IValidator<TCommand>> validators) : ICommandValidator<TCommand>
    where TCommand : ICommand
{
    public async ValueTask ValidateAsync(TCommand command)
    {
        var validatorList = validators.ToArray();

        if (validatorList.Length == 0)
        {
            return;
        }

        var context = new ValidationContext<TCommand>(command);
        var results = await Task.WhenAll(
            validatorList.Select(validator => validator.ValidateAsync(context)));
        var failures = results
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToArray();

        if (failures.Length != 0)
        {
            throw new ValidationException(failures);
        }
    }
}
