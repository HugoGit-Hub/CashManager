using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.ShoppingSessions;

public interface IShoppingSessionService
{
    public Task<Result<ShoppingSession>> GetShoppinsSessionById(int id, CancellationToken cancellationToken);

    public Task<Result> CreateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken);

    public Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken);
}