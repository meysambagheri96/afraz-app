using MediatR;

namespace Afraz.Application.Features.Foundation.GetStatus;

public sealed record GetStatusQuery : IRequest<GetStatusResponse>;

public sealed record GetStatusResponse(string Service, string Status);

internal sealed class GetStatusHandler : IRequestHandler<GetStatusQuery, GetStatusResponse>
{
    public Task<GetStatusResponse> Handle(
        GetStatusQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetStatusResponse("Afraz.Api", "ready"));
    }
}
