using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.Transactions;

public static class TransactionErrors
{
    public static Error WrongTransactionState(TransactionStateEnum state) => new(
        "Transaction.WrongTransaction", $"Wrong transaction state {state}");

    public static readonly Error NotFoundTransactionError = new(
        "Transaction.NotFound", "Transaction not found");

    public static readonly Error WrongSignatureError = new(
        "Transaction.WrongSignature", "Transactions signatures are not the same");
}