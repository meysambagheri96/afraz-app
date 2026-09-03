using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Afraz.Application.Features.Authentication;

namespace Afraz.Api.Authentication;

public sealed class HttpCurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public int UserId
    {
        get
        {
            var principal = accessor.HttpContext?.User;
            var value = principal?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(value, out var userId)
                ? userId
                : throw new AuthenticationException("The current user is not authenticated.");
        }
    }
}
