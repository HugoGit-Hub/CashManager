using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;

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

    public async Task<Result<ShoppingSession>> CreateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        var createdShoppingSession = await _shoppingSessionRepository.CreateShoppingSession(shoppingSession, cancellationToken);

        return Result<ShoppingSession>.Success(createdShoppingSession.Value);
    }

    public async Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        await _shoppingSessionRepository.UpdateShoppingSession(shoppingSession, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<ShoppingSession>> GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(Users user, CancellationToken cancellationToken)
    {
        var asShoppingSessionOpen = user.ShoppingSessions.All(x => x.State is false) && user.ShoppingSessions.Count is not 0;
        if (!asShoppingSessionOpen)
        {
            var createshoppingSession = new ShoppingSession
            {
                State = false,
                TotalPrice = 0,
                UserId = user.Id,
                User = user
            };

            var createdShoppingSession = await CreateShoppingSession(createshoppingSession, cancellationToken);

            return createdShoppingSession.IsFailure
                ? Result<ShoppingSession>.Failure(createdShoppingSession.Error)
                : Result<ShoppingSession>.Success(createdShoppingSession.Value);
        }

        var shoppingSessionId = user.ShoppingSessions.SingleOrDefault(x => x.State is false);
        if (shoppingSessionId is null)
        {
            return Result<ShoppingSession>.Failure(ShoppingSessionErrors.NotFound);
        }

        var shoppingSession = await GetShoppinsSessionById(shoppingSessionId.Id, cancellationToken);

        return shoppingSession.IsFailure
            ? Result<ShoppingSession>.Failure(shoppingSession.Error)
            : Result<ShoppingSession>.Success(shoppingSession.Value);
    }
}