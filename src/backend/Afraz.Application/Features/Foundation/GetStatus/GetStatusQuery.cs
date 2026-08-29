using Infra.Queries;

namespace Afraz.Application.Features.Foundation.GetStatus;

public sealed record GetStatusQuery : IQueryResult<GetStatusResponse>;

public sealed record GetStatusResponse(string Service, string Status);

internal sealed class GetStatusHandler : IQueryHandler<GetStatusQuery, GetStatusResponse>
{
    public Task<GetStatusResponse> HandleAsync(
        GetStatusQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetStatusResponse("Afraz.Api", "ready"));
    }
}
