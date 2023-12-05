using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
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

    public Result<string> GetClaim(string claimType)
    {
        var result = CurrentUser.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return result is null 
            ? Result<string>.Failure(CurrentUserErrors.ClaimTypeNullError) 
            : Result<string>.Success(result);
    }
}