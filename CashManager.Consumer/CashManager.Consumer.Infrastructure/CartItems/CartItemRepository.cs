using CashManager.Consumer.Application.CartItems;
using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Infrastructure.Context;

namespace CashManager.Consumer.Infrastructure.CartItems;

internal class CartItemRepository : ICartItemRepository
{
    private readonly CashManagerConsumerContext _context;

    public CartItemRepository(CashManagerConsumerContext context)
    {
        _context = context;
    }

    public async Task<CartItem> Create(CartItem cartItem, CancellationToken cancellationToken)
    {
        var createCartItem = await _context.CartItems.AddAsync(cartItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return createCartItem.Entity;
    }
}