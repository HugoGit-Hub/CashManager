using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Application.CartItems;

internal class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemService(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<Result<CartItem>> Create(CartItem cartItem, CancellationToken cancellationToken)
    {
        var createCarteItem = await _cartItemRepository.Create(cartItem, cancellationToken);

        return Result<CartItem>.Success(createCarteItem);
    }
}