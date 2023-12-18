using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.CartItems;

public interface ICartItemService
{
    public Task<Result<CartItem>> Create(CartItem cartItem, CancellationToken cancellationToken);
}