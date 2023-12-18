using CashManager.Consumer.Domain.CartItems;
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

    public async Task<Result<IEnumerable<CartItem>>> GetShoppingSessionCartItems(int id)
    {
        var shoppingSession = await _shoppingSessionRepository.GetShoppingSession(id);
        
        return shoppingSession is null 
            ? Result<IEnumerable<CartItem>>.Failure(ShoppingSessionErrors.NotFound) 
            : Result<IEnumerable<CartItem>>.Success(shoppingSession.CartItems);
    }
}