using CashManager.Consumer.Domain.ShoppingSessions;

namespace CashManager.Consumer.Application.ShoppingSessions;

public interface IShoppingSessionRepository
{
    public Task<ShoppingSession?> GetShoppingSession(int id);
}