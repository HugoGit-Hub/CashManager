using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
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
        var shoppinsSession = await _shoppingSessionService.GetShoppinsSessionById(request.Id, cancellationToken);
        var getShoppingSessionCartItemsResponseList = shoppinsSession.Value.CartItems
            .Select(cartItem => 
                new GetShoppingSessionCartItemsResponse 
                {
                    ArticleName = cartItem.Article.Name, 
                    Quantity = cartItem.Quantity
                })
            .ToList();

        return shoppinsSession.IsFailure 
            ? Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Failure(shoppinsSession.Error)
            : Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Success(getShoppingSessionCartItemsResponseList);
    }
}