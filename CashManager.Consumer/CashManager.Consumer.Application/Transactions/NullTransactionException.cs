namespace CashManager.Consumer.Application.Transactions;

public class NullTransactionException : Exception
{
    public NullTransactionException()
    {
    }

    public NullTransactionException(string message)
    : base(message)
    {
    }

    public NullTransactionException(string message, Exception inner)
    : base(message, inner)
    {
    }
}