using CashManager.Consumer.Application.ShoppingSessions;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Consumer.Infrastructure.ShoppingSessions;

internal class ShoppingSessionRepository : IShoppingSessionRepository
{
    private readonly CashManagerConsumerContext _context;

    public ShoppingSessionRepository(CashManagerConsumerContext context)
    {
        _context = context;
    }

    public async Task<ShoppingSession?> GetShoppingSession(int id, CancellationToken cancellationToken)
    {
        var shoppingSession = await _context.ShoppingSessions
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Article)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return shoppingSession;
    }

    public async Task<Result> CreateShoppingSession(
        ShoppingSession shoppingSession,
        CancellationToken cancellationToken)
    {
        await _context.ShoppingSessions.AddAsync(shoppingSession, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        _context.ShoppingSessions.Update(shoppingSession);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}