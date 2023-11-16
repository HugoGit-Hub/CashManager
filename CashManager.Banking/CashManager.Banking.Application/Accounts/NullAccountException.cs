namespace CashManager.Banking.Application.Accounts;

public class NullAccountException : Exception
{
    public NullAccountException()
    {
    }

    public NullAccountException(string message)
        : base(message)
    {
    }

    public NullAccountException(string message, Exception inner)
        : base(message, inner)
    {
    }
}