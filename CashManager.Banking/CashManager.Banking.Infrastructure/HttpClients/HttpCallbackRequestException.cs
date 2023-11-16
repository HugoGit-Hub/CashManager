namespace CashManager.Banking.Infrastructure.HttpClients;

public class HttpCallbackRequestException : Exception
{
    public HttpCallbackRequestException()
    {
    }

    public HttpCallbackRequestException(string? message)
        : base(message)
    {
    }

    public HttpCallbackRequestException(string? message, Exception inner)
        : base(message, inner)
    {
    }
}