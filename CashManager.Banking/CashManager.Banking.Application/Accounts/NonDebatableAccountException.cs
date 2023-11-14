namespace CashManager.Banking.Application.Accounts;

public class NonDebatableAccountException : Exception
{
    public NonDebatableAccountException()
    {
    }

    public NonDebatableAccountException(string message)
        : base(message)
    {
    }

    public NonDebatableAccountException(string message, Exception inner)
        : base(message, inner)
    {
    }
}