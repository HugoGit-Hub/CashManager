using CashManager.Banking.Domain.CurrentUser;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CashManager.Banking.Infrastructure.CurrentUser;

internal class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private ClaimsPrincipal CurrentUser => _httpContextAccessor.HttpContext.User;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClaim(string claimType)
    {
        return CurrentUser.Claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? throw new ClaimTypeNullException($"Claim type provided is null : {claimType}");
    }
}