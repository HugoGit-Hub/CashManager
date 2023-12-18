using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.ShoppingSessions;

public static class ShoppingSessionErrors
{
    public static readonly Error NotFound = new(
        "ShoppingSession.NotFound", "Unable to found shopping session");
}