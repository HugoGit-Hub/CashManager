namespace CashManager.Banking.Application.Transactions;

public class WrongSignatureException : Exception
{
    public WrongSignatureException()
    {
    }

    public WrongSignatureException(string message)
        : base(message)
    {
    }

    public WrongSignatureException(string message, Exception inner)
        : base(message, inner)
    {
    }
}