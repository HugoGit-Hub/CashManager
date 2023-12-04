using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.Transactions;

public static class TransactionErrors
{
    public static readonly Error TransactionNotFound = new(
        "Transaction.NotFound", "Unable to found transaction");
}