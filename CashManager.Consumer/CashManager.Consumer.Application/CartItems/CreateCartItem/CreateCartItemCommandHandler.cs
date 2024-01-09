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
        
        var currentShoppingSession = await _shoppingSessionService.GetCurrentShoppingSession(cancellationToken);
        if (currentShoppingSession.IsFailure)
        {
            return Result<CreateCartItemResponse>.Failure(currentShoppingSession.Error);
        }
        var isCartItemPresent = currentShoppingSession.Value.CartItems.Any(x => x.Article.Id == request.CreateCartItemRequest.IdArticle);
        if (isCartItemPresent)
        {
            var currentCartItem = currentShoppingSession.Value.CartItems.FirstOrDefault(x => x.Article.Id == request.CreateCartItemRequest.IdArticle);
            if (currentCartItem is null)
            {
                return Result<CreateCartItemResponse>.Failure(ShoppingSessionErrors.NotFound);
            }

            await _shoppingSessionService.UpdateOrDeleteCartItemInCurrentShoppingSession(request.CreateCartItemRequest.Quantity, currentCartItem, currentShoppingSession.Value, cancellationToken);
            if (currentShoppingSession.IsFailure)
            {
                return Result<CreateCartItemResponse>.Failure(currentShoppingSession.Error);
            }

            var createCarteItemResponsee = new CreateCartItemResponse
            {
                Quantity = request.CreateCartItemRequest.Quantity,
                ArticleName = article.Value.Name
            };

            return Result<CreateCartItemResponse>.Success(createCarteItemResponsee);
        }
        currentShoppingSession.Value.TotalPrice += request.CreateCartItemRequest.Quantity * article.Value.Price;
        await _shoppingSessionService.UpdateShoppingSession(currentShoppingSession.Value, cancellationToken);

        var cartItem = new CartItem
        {
            Quantity = request.CreateCartItemRequest.Quantity,
            Article = article.Value,
            ShoppingSession = currentShoppingSession.Value
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
}