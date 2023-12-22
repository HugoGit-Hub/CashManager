using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.CartItems;

public interface ICartItemService
{
    public Task<Result<CartItem>> Create(CartItem cartItem, CancellationToken cancellationToken);

    public Task<Result<CartItem>> GetById(int cartItemId, CancellationToken cancellationToken);

    public Task<Result<CartItem>> Update(CartItem cartItem, CancellationToken cancellationToken);
}