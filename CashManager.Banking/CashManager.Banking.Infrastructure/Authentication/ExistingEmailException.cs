namespace CashManager.Banking.Infrastructure.Authentication;

public class ExistingEmailException : Exception
{
    public ExistingEmailException()
    {
    }

    public ExistingEmailException(string message)
        : base(message)
    {
    }

    public ExistingEmailException(string message, Exception inner)
        : base(message, inner)
    {
    }
}