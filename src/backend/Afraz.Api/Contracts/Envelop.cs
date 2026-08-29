using System.Net;

namespace Afraz.Api.Contracts;

public sealed class Envelop<T>
{
    public required ApiMetadata Meta { get; init; }

    public T? Data { get; init; }

    public PaginationInfo? Pagination { get; init; }

    public static Envelop<T> Success(
        HttpStatusCode code,
        T data,
        PaginationInfo? pagination = null)
    {
        return new Envelop<T>
        {
            Meta = new ApiMetadata(code),
            Data = data,
            Pagination = pagination,
        };
    }

    public static Envelop<T> HandledError(
        HttpStatusCode code,
        IReadOnlyCollection<ApiErrorEntry>? errors = null,
        string? errorMessage = null)
    {
        return new Envelop<T>
        {
            Meta = new ApiMetadata(code, errorMessage, errors),
        };
    }
}
