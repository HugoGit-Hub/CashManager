namespace CashManager.Banking.Infrastructure.CurrentUser;

public class ClaimTypeNullException : Exception
{
    public ClaimTypeNullException()
    {
    }

    public ClaimTypeNullException(string message)
        : base(message)
    {
    }

    public ClaimTypeNullException(string message, Exception inner)
        : base(message, inner)
    {
    }
}