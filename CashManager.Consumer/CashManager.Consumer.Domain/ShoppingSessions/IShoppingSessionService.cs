using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.ShoppingSessions;

public interface IShoppingSessionService
{
    public Task<Result<IEnumerable<CartItem>>> GetShoppingSessionCartItems(int id);
}