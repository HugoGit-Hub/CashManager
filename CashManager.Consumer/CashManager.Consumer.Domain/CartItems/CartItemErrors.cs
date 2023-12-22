using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.CartItems;

public static class CartItemErrors
{
    public static readonly Error NotFound = new(
        "CartItem.NotFound", "Unable to found cart item");
}
