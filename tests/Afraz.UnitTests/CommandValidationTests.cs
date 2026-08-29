using Afraz.Application;
using FluentAssertions;
using FluentValidation;
using Infra.Commands;
using Xunit;

namespace Afraz.UnitTests;

public sealed class CommandValidationTests
{
    [Fact]
    public async Task ValidateAsync_WithInvalidCommand_ShouldThrowValidationException()
    {
        var sut = new FluentValidationCommandValidator<TestCommand>([new TestCommandValidator()]);

        Func<Task> action = async () => await sut.ValidateAsync(new TestCommand(string.Empty));

        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ValidateAsync_WithValidCommand_ShouldComplete()
    {
        var sut = new FluentValidationCommandValidator<TestCommand>([new TestCommandValidator()]);

        await sut.ValidateAsync(new TestCommand("valid"));
    }

    private sealed record TestCommand(string Value) : ICommand;

    private sealed class TestCommandValidator : AbstractValidator<TestCommand>
    {
        public TestCommandValidator()
        {
            RuleFor(command => command.Value).NotEmpty();
        }
    }
}
