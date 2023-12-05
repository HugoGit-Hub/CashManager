using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.Accounts;

public static class AccountError
{
    public static readonly Error AccountNotFoundError = new(
        "Account.NotFound", "No account found");

    public static Error NonDebatableAccountError(string account, double amount) => new(
        "Account.NonDebatableAccount", $"Creditor account : {account}, can't be debited of : {amount}");
}