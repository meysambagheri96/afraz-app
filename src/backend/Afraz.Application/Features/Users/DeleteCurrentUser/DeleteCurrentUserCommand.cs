using Afraz.Application.Features.Authentication;
using Infra.Commands;

namespace Afraz.Application.Features.Users.DeleteCurrentUser;

public sealed record DeleteCurrentUserCommand : ICommand;
public sealed record DeleteCurrentUserResponse(bool Deactivated);

internal sealed class DeleteCurrentUserHandler(IAuthRepository repository, ICurrentUser currentUser)
    : ICommandHandler<DeleteCurrentUserCommand, DeleteCurrentUserResponse>
{
    public async Task<DeleteCurrentUserResponse> HandleAsync(DeleteCurrentUserCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.FindByIdAsync(currentUser.UserId, cancellationToken)
            ?? throw new AuthenticationException("User was not found.");
        user.Deactivate(DateTime.UtcNow);
        await repository.SaveChangesAsync(cancellationToken);
        return new DeleteCurrentUserResponse(true);
    }
}
