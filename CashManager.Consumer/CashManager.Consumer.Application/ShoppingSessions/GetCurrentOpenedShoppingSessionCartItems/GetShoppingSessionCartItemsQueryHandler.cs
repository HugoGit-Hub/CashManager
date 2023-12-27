using System.Security.Claims;
using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;

internal class GetShoppingSessionCartItemsQueryHandler : IRequestHandler<GetShoppingSessionCartItemsQuery, Result<IEnumerable<GetShoppingSessionCartItemsResponse>>>
{
    private readonly IShoppingSessionService _shoppingSessionService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public GetShoppingSessionCartItemsQueryHandler(
        IShoppingSessionService shoppingSessionService,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _shoppingSessionService = shoppingSessionService;
        _currentUserService = currentUserService;
        _userService = userService;
    }

    public async Task<Result<IEnumerable<GetShoppingSessionCartItemsResponse>>> Handle(GetShoppingSessionCartItemsQuery request, CancellationToken cancellationToken)
    {
        var currentUserEmail = _currentUserService.GetClaim(ClaimTypes.Email);
        if (currentUserEmail.IsFailure)
        {
            return Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Failure(currentUserEmail.Error);
        }

        var currentUser = await _userService.GetUserByEmail(currentUserEmail.Value, cancellationToken);
        if (currentUser.IsFailure)
        {
            return Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Failure(currentUser.Error);
        }

        var currentShoppingSession = await _shoppingSessionService.GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(currentUser.Value, cancellationToken);
        if (currentShoppingSession.IsFailure)
        {
            return Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Failure(currentShoppingSession.Error);
        }
        
        var shoppinsSession = await _shoppingSessionService.GetShoppinsSessionById(currentShoppingSession.Value.Id, cancellationToken);
        var getShoppingSessionCartItemsResponseList = shoppinsSession.Value.CartItems
            .Select(cartItem => 
                new GetShoppingSessionCartItemsResponse 
                {
                    Id = cartItem.Id,
                    ArticleName = cartItem.Article.Name, 
                    Quantity = cartItem.Quantity,
                    TotalArticlePrice = Math.Round(cartItem.Quantity * cartItem.Article.Price, 2),
                    ImageUrl = cartItem.Article.ImageUrl,
                    ArticleId = cartItem.Article.Id
                })
            .ToList();

        return shoppinsSession.IsFailure 
            ? Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Failure(shoppinsSession.Error)
            : Result<IEnumerable<GetShoppingSessionCartItemsResponse>>.Success(getShoppingSessionCartItemsResponseList);
    }
}