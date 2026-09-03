namespace Afraz.Application.Common.Validation;

public sealed class RequestValidationException(IReadOnlyDictionary<string, string[]> errors)
    : Exception("One or more validation errors occurred.")
{
    public IReadOnlyDictionary<string, string[]> Errors { get; } = errors;
}

public static class ValidationFailure
{
    public static void ThrowIf(bool condition, string propertyName, string message)
    {
        if (!condition) return;
        throw new RequestValidationException(new Dictionary<string, string[]>
        {
            [propertyName] = [message],
        });
    }
}
