namespace CashManager.Banking.Application.Transactions;

public class UserAccountNotFoundException : Exception
{
    public UserAccountNotFoundException()
    {
    }
    
    public UserAccountNotFoundException(string message)
        : base(message)
    {
    }

    public UserAccountNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}