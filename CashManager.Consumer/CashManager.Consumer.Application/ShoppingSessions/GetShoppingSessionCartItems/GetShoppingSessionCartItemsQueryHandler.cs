using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using Mapster;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.GetShoppingSessionCartItems;

internal class GetShoppingSessionCartItemsQueryHandler : IRequestHandler<GetShoppingSessionCartItemsQuery, Result<IEnumerable<GetShoppingSessionCartItemsResponse>>>
{
    private readonly IShoppingSessionService _shoppingSessionService;

    public GetShoppingSessionCartItemsQueryHandler(IShoppingSessionService shoppingSessionService)
    {
        _shoppingSessionService = shoppingSessionService;
    }

    public async Task<Result<IEnumerable<GetShoppingSessionCartItemsResponse>>> Handle(GetShoppingSessionCartItemsQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await _shoppingSessionService.GetShoppinsSessionById(request.Id, cancellationToken);
        
        return cartItems.IsFailure 
            ? Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Failure(cartItems.Error)
            : Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Success(cartItems.Value.CartItems.Adapt<IEnumerable<GetShoppingSessionCartItemsResponse>>());
    }
}