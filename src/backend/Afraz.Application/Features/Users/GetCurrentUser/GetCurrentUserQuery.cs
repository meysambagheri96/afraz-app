using Afraz.Application.Features.Authentication;
using Infra.Queries;

namespace Afraz.Application.Features.Users.GetCurrentUser;

public sealed record GetCurrentUserQuery : IQueryResult<UserResponse>;

internal sealed class GetCurrentUserHandler(IAuthRepository repository, ICurrentUser currentUser)
    : IQueryHandler<GetCurrentUserQuery, UserResponse>
{
    public async Task<UserResponse> HandleAsync(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.FindByIdAsync(currentUser.UserId, cancellationToken);
        if (user is null || !user.IsActive) throw new AuthenticationException("User was not found.");
        return UserResponseMapper.Map(user);
    }
}
