using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.CurrentUser;

public static class CurrentUserErrors
{
    public static readonly Error ClaimTypeNullError = new(
        "CurrentUser.ClaimTypeNull", "Claim type provided is null");
}