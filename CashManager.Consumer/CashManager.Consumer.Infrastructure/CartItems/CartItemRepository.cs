using CashManager.Consumer.Application.CartItems;
using CashManager.Consumer.Domain.CartItems;
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

    public async Task<CartItem?> GetById(int cartItemId, CancellationToken cancellationToken)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId, cancellationToken);

        return cartItem;
    }

    public async Task<CartItem?> Update(CartItem cartItem, CancellationToken cancellationToken)
    {
        var updateCartItem = _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync(cancellationToken);

        return updateCartItem.Entity;
    }
}