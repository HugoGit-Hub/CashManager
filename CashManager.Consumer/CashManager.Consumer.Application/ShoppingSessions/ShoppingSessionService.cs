using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;

namespace CashManager.Consumer.Application.ShoppingSessions;

internal class ShoppingSessionService : IShoppingSessionService
{
    private readonly IShoppingSessionRepository _shoppingSessionRepository;

    public ShoppingSessionService(IShoppingSessionRepository shoppingSessionRepository)
    {
        _shoppingSessionRepository = shoppingSessionRepository;
    }

    public async Task<Result<ShoppingSession>> GetShoppinsSessionById(int id, CancellationToken cancellationToken)
    {
        var shoppingSession = await _shoppingSessionRepository.GetShoppingSession(id, cancellationToken);

        return shoppingSession is null
            ? Result<ShoppingSession>.Failure(ShoppingSessionErrors.NotFound)
            : Result<ShoppingSession>.Success(shoppingSession);
    }

    public async Task<Result> CreateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        await _shoppingSessionRepository.CreateShoppingSession(shoppingSession, cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        await _shoppingSessionRepository.UpdateShoppingSession(shoppingSession, cancellationToken);

        return Result.Success();
    }
}