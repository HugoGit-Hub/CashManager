namespace CashManager.Banking.Infrastructure.CurrentUser;

public interface ICurrentUserService
{
    public string? GetClaim(string claimType);
}