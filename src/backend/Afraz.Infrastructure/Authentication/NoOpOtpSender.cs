using Afraz.Application.Features.Authentication;

namespace Afraz.Infrastructure.Authentication;

public sealed class NoOpOtpSender : IOtpSender
{
    public Task SendAsync(string dialingCode, string phone, string code, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
