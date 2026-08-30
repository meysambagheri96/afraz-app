using FluentValidation;
using Infra.Commands;

namespace Afraz.UnitTests;

public sealed class CommandValidationTests
{
    private sealed record TestCommand(string Value) : ICommand;

    private sealed class TestCommandValidator : AbstractValidator<TestCommand>
    {
        public TestCommandValidator()
        {
            RuleFor(command => command.Value).NotEmpty();
        }
    }
}
