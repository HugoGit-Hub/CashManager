namespace CashManager.Banking.Application.Transactions;

public class BadTransactionStateException : Exception
{
    public BadTransactionStateException()
    {
    }

    public BadTransactionStateException(string message)
        : base(message)
    {
    }

    public BadTransactionStateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}