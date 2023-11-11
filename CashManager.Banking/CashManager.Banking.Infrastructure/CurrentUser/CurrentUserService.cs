using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CashManager.Banking.Infrastructure.CurrentUser;

internal class CurrentUserService : ICurrentUserService
{
    private readonly IEnumerable<Claim> _claims;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _claims = httpContextAccessor.HttpContext.User.Claims;
    }

    public string? GetClaim(string claimType)
    {
        return _claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}