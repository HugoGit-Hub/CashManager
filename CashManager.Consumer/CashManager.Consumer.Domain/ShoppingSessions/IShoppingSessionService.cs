using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.ShoppingSessions;

public interface IShoppingSessionService
{
    public Task<Result<ShoppingSession>> GetShoppinsSessionById(int id, CancellationToken cancellationToken);
}