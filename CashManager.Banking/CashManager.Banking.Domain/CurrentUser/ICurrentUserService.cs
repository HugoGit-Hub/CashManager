using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.CurrentUser;

public interface ICurrentUserService
{
    public Result<string> GetClaim(string claimType);
}