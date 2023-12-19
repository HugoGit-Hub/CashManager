using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;
using MediatR;
using System.Security.Claims;

namespace CashManager.Consumer.Application.CartItems.CreateCartItem;

internal class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result<CreateCartItemResponse>>
{
    private readonly ICartItemService _cartItemService;
    private readonly IArticleService _articleService;
    private readonly IShoppingSessionService _shoppingSessionService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public CreateCartItemCommandHandler(
        ICartItemService cartItemService,
        IArticleService articleService,
        IShoppingSessionService shoppingSessionService,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _cartItemService = cartItemService;
        _articleService = articleService;
        _shoppingSessionService = shoppingSessionService;
        _currentUserService = currentUserService;
        _userService = userService;
    }

    public async Task<Result<CreateCartItemResponse>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleService.Get(request.CreateCartItemRequest.IdArticle, cancellationToken);
        if (article.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(article.Error);
        }

        var currentUserEmail = _currentUserService.GetClaim(ClaimTypes.Email);
        if (currentUserEmail.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(currentUserEmail.Error);
        }

        var user = await _userService.GetUserByEmail(currentUserEmail.Value, cancellationToken);
        if (user.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(user.Error);
        }

        var shoppingSession = await CreateShoppingSessionIfNullOrNotOpenShoppingSession(user.Value, cancellationToken);
        if (shoppingSession.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(shoppingSession.Error);
        }

        shoppingSession.Value.TotalPrice += request.CreateCartItemRequest.Quantity * article.Value.Price;
        await _shoppingSessionService.UpdateShoppingSession(shoppingSession.Value, cancellationToken);

        var cartItem = new CartItem
        {
            Quantity = request.CreateCartItemRequest.Quantity,
            Article = article.Value,
            ShoppingSession = shoppingSession.Value
        };

        var createdCartItem = await _cartItemService.Create(cartItem, cancellationToken);
        var createCarteItemResponse = new CreateCartItemResponse
        {
            Quantity = request.CreateCartItemRequest.Quantity,
            ArticleName = article.Value.Name
        };
        
        return createdCartItem.IsFailure 
            ? Result<CreateCartItemResponse>.Failure(createdCartItem.Error) 
            : Result<CreateCartItemResponse>.Success(createCarteItemResponse);
    }

    private async Task<Result<ShoppingSession>> CreateShoppingSessionIfNullOrNotOpenShoppingSession(Users user, CancellationToken cancellationToken)
    {
        var asShoppingSessionOpen = user.ShoppingSessions.All(x => x.State is false);
        if (!asShoppingSessionOpen)
        {
            var createshoppingSession = new ShoppingSession
            {
                State = false,
                TotalPrice = 0,
                UserId = user.Id,
                User = user
            };

            var createdShoppingSession = await _shoppingSessionService.CreateShoppingSession(createshoppingSession, cancellationToken);
            
            return createdShoppingSession.IsFailure 
                ? Result<ShoppingSession>.Failure(createdShoppingSession.Error) 
                : Result<ShoppingSession>.Success(createdShoppingSession.Value);
        }

        var shoppingSessionId = user.ShoppingSessions.SingleOrDefault(x => x.State is false);
        if (shoppingSessionId is null)
        {
            return Result<ShoppingSession>.Failure(ShoppingSessionErrors.NotFound);
        }

        var shoppingSession = await _shoppingSessionService.GetShoppinsSessionById(shoppingSessionId.Id, cancellationToken);
        
        return shoppingSession.IsFailure 
            ? Result<ShoppingSession>.Failure(shoppingSession.Error) 
            : Result<ShoppingSession>.Success(shoppingSession.Value);
    }
}