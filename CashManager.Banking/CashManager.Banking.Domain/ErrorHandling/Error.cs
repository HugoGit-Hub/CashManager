namespace CashManager.Banking.Domain.ErrorHandling;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}