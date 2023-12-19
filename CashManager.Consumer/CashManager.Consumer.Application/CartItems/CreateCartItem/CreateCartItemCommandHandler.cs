using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using MediatR;

namespace CashManager.Consumer.Application.CartItems.CreateCartItem;

internal class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, Result<CreateCartItemResponse>>
{
    private readonly ICartItemService _cartItemService;
    private readonly IArticleService _articleService;
    private readonly IShoppingSessionService _shoppingSessionService;

    public CreateCartItemCommandHandler(
        ICartItemService cartItemService,
        IArticleService articleService,
        IShoppingSessionService shoppingSessionService)
    {
        _cartItemService = cartItemService;
        _articleService = articleService;
        _shoppingSessionService = shoppingSessionService;
    }

    public async Task<Result<CreateCartItemResponse>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleService.Get(request.CreateCartItemRequest.IdArticle, cancellationToken);
        if (article.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(article.Error);
        }

        var shoppingSession = await _shoppingSessionService.GetShoppinsSessionById(request.CreateCartItemRequest.IdShoppingSession, cancellationToken);
        if (shoppingSession.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(shoppingSession.Error);
        }

        var cartItem = new CartItem
        {
            Quantity = request.CreateCartItemRequest.Quantity,
            Article = article.Value,
            ShoppingSession = shoppingSession.Value
        };

        shoppingSession.Value.TotalPrice = request.CreateCartItemRequest.Quantity * article.Value.Price;
        await _shoppingSessionService.UpdateShoppingSession(shoppingSession.Value, cancellationToken);

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
}