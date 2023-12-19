using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;

namespace CashManager.Consumer.Application.ShoppingSessions;

public interface IShoppingSessionRepository
{
    public Task<ShoppingSession?> GetShoppingSession(int id, CancellationToken cancellationToken);

    public Task<Result<ShoppingSession>> CreateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken);

    public Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken);
}