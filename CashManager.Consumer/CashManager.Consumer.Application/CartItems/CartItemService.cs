using CashManager.Consumer.Domain.Articles;
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

    public async Task<Result<CartItem>> GetById(int cartItemId, CancellationToken cancellationToken)
    {
        var getCartItem = await _cartItemRepository.GetById(cartItemId, cancellationToken);
        
        return getCartItem is null 
            ? Result<CartItem>.Failure(ArticleErrors.ArticleNotFound) 
            : Result<CartItem>.Success(getCartItem);
    }

    public async Task<Result<CartItem>> Update(CartItem cartItem, CancellationToken cancellationToken)
    {
        var updateCartItem = await _cartItemRepository.Update(cartItem, cancellationToken);
        
        return updateCartItem is null 
            ? Result<CartItem>.Failure(CartItemErrors.NotFound) 
            : Result<CartItem>.Success(updateCartItem);
    }
}