using CashManager.Consumer.Domain.CartItems;

namespace CashManager.Consumer.Application.CartItems;

public interface ICartItemRepository
{
    public Task<CartItem> Create(CartItem cartItem, CancellationToken cancellationToken);

    public Task<CartItem?> GetById(int cartItemId, CancellationToken cancellationToken);

    public Task<CartItem?> Update(CartItem cartItem, CancellationToken cancellationToken);
}