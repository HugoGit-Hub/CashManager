using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.CurrentUser;

public static class CurrentUserErrors
{
    public static readonly Error ClaimTypeNullError = new(
        "CurrentUser.ClaimTypeNull", "Claim type provided is null");
}