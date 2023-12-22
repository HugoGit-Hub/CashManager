using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.UpdateCartItemFromCurrentShoppingSession;

internal class UpdateCartItemFromCurrentShoppingSessionCommandHandler : IRequestHandler<UpdateCartItemFromCurrentShoppingSessionCommand, Result>
{
    private readonly ICartItemService _cartItemService;

    public UpdateCartItemFromCurrentShoppingSessionCommandHandler(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    public async Task<Result> Handle(UpdateCartItemFromCurrentShoppingSessionCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemService.GetById(request.CartItemId, cancellationToken);
        if (cartItem.IsFailure)
        {
            return Result.Failure(cartItem.Error);
        }

        cartItem.Value.Quantity = request.Quantity;

        var updateCartItem = await _cartItemService.Update(cartItem.Value, cancellationToken);
        
        return updateCartItem.IsFailure 
            ? Result.Failure(updateCartItem.Error) 
            : Result.Success();
    }
}