namespace CashManager.Banking.Domain.CurrentUser;

public interface ICurrentUserService
{
    public string GetClaim(string claimType);
}