using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.DeleteCartItemFromCurrentShoppingSession;

internal class DeleteCartItemFromCurrentShoppingSessionCommandHandler : IRequestHandler<DeleteCartItemFromCurrentShoppingSessionCommand, Result>
{
    private readonly IShoppingSessionService _shoppingSessionService;
    private readonly ICartItemService _cartItemService;

    public DeleteCartItemFromCurrentShoppingSessionCommandHandler(
        IShoppingSessionService shoppingSessionService,
        ICartItemService cartItemService)
    {
        _shoppingSessionService = shoppingSessionService;
        _cartItemService = cartItemService;
    }

    public async Task<Result> Handle(DeleteCartItemFromCurrentShoppingSessionCommand request, CancellationToken cancellationToken)
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

        var deleteCartItemFromShoppingSession = await _shoppingSessionService.UpdateOrDeleteCartItemInCurrentShoppingSession(0, cartItem.Value, currentShoppingSession.Value, cancellationToken);

        return deleteCartItemFromShoppingSession.IsFailure
            ? Result.Failure(deleteCartItemFromShoppingSession.Error)
            : Result.Success();
    }
}