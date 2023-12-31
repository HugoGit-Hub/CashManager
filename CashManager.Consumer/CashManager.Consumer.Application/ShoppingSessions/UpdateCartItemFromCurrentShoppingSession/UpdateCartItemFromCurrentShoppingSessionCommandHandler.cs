using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.UpdateCartItemFromCurrentShoppingSession;

internal class UpdateCartItemFromCurrentShoppingSessionCommandHandler : IRequestHandler<UpdateCartItemFromCurrentShoppingSessionCommand, Result>
{
    private readonly ICartItemService _cartItemService;
    private readonly IShoppingSessionService _shoppingSessionService;

    public UpdateCartItemFromCurrentShoppingSessionCommandHandler(
        ICartItemService cartItemService,
        IShoppingSessionService shoppingSessionService)
    {
        _cartItemService = cartItemService;
        _shoppingSessionService = shoppingSessionService;
    }

    public async Task<Result> Handle(UpdateCartItemFromCurrentShoppingSessionCommand request, CancellationToken cancellationToken)
    {
        var currentShoppingSession = await _shoppingSessionService.GetCurrentShoppingSession(cancellationToken);
        if (currentShoppingSession.IsFailure)
        {
            return Result<ShoppingSession>.Failure(currentShoppingSession.Error);
        }

        var cartItem = await _cartItemService.GetById(request.CartItemId, cancellationToken);
        if (cartItem.IsFailure)
        {
            return Result.Failure(cartItem.Error);
        }

        var updateCartItem = await _shoppingSessionService.UpdateOrDeleteCartItemInCurrentShoppingSession(request.Quantity, cartItem.Value, currentShoppingSession.Value, cancellationToken);

        return updateCartItem.IsFailure 
            ? Result.Failure(updateCartItem.Error) 
            : Result.Success();
    }
}