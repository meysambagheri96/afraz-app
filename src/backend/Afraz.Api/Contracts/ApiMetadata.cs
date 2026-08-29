using System.Net;

namespace Afraz.Api.Contracts;

public sealed record ApiMetadata(
    HttpStatusCode Code,
    string? ErrorMessage = null,
    IReadOnlyCollection<ApiErrorEntry>? Errors = null);
