using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.User;

namespace CashManager.Consumer.Domain.ShoppingSessions;

public interface IShoppingSessionService
{
    public Task<Result<ShoppingSession>> GetShoppinsSessionById(int id, CancellationToken cancellationToken);

    public Task<Result<ShoppingSession>> CreateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken);

    public Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken);

    public Task<Result<ShoppingSession>> GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(Users user, CancellationToken cancellationToken);

    public Task<Result<ShoppingSession>> DeleteCartItemFromCurrentShoppingSession(int shoppingSessionId, int cartItemId, CancellationToken cancellationToken);
}