using CashManager.Consumer.Application.ShoppingSessions;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Infrastructure.Context;

namespace CashManager.Consumer.Infrastructure.ShoppingSessions;

internal class ShoppingSessionRepository : IShoppingSessionRepository
{
    private readonly CashManagerConsumerContext _context;

    public ShoppingSessionRepository(CashManagerConsumerContext context)
    {
        _context = context;
    }

    public async Task<ShoppingSession?> GetShoppingSession(int id)
    {
        var cartItems = await _context.ShoppingSessions.FindAsync(id);

        return cartItems;
    }
}