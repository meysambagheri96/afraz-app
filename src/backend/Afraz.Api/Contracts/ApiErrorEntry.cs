namespace Afraz.Api.Contracts;

public sealed record ApiErrorEntry(
    string Key,
    int? ErrorCode,
    IReadOnlyCollection<string> Errors);
