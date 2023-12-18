using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Application.CurrentUser;

public interface ICurrentUserService
{
    public Result<string> GetClaim(string claimType);
}