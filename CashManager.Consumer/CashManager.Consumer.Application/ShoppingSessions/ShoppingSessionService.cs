using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;
using System.Security.Claims;

namespace CashManager.Consumer.Application.ShoppingSessions;

internal class ShoppingSessionService : IShoppingSessionService
{
    private readonly IShoppingSessionRepository _shoppingSessionRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;
    private readonly ICartItemService _cartItemService;

    public ShoppingSessionService(
        IShoppingSessionRepository shoppingSessionRepository,
        ICurrentUserService currentUserService,
        IUserService userService,
        ICartItemService cartItemService)
    {
        _shoppingSessionRepository = shoppingSessionRepository;
        _currentUserService = currentUserService;
        _userService = userService;
        _cartItemService = cartItemService;
    }

    public async Task<Result<ShoppingSession>> GetShoppinsSessionById(int id, CancellationToken cancellationToken)
    {
        var shoppingSession = await _shoppingSessionRepository.GetShoppingSession(id, cancellationToken);

        return shoppingSession is null
            ? Result<ShoppingSession>.Failure(ShoppingSessionErrors.NotFound)
            : Result<ShoppingSession>.Success(shoppingSession);
    }

    public async Task<Result<ShoppingSession>> CreateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        var createdShoppingSession = await _shoppingSessionRepository.CreateShoppingSession(shoppingSession, cancellationToken);

        return Result<ShoppingSession>.Success(createdShoppingSession.Value);
    }

    public async Task<Result> UpdateShoppingSession(ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        await _shoppingSessionRepository.UpdateShoppingSession(shoppingSession, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<ShoppingSession>> GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(Users user, CancellationToken cancellationToken)
    {
        var asShoppingSessionOpen = user.ShoppingSessions.Any(x => x.State is false) && user.ShoppingSessions.Count > 0;
        if (!asShoppingSessionOpen)
        {
            var createshoppingSession = new ShoppingSession
            {
                State = false,
                TotalPrice = 0,
                UserId = user.Id,
                User = user
            };

            var createdShoppingSession = await CreateShoppingSession(createshoppingSession, cancellationToken);

            return createdShoppingSession.IsFailure
                ? Result<ShoppingSession>.Failure(createdShoppingSession.Error)
                : Result<ShoppingSession>.Success(createdShoppingSession.Value);
        }

        var shoppingSessionId = user.ShoppingSessions.SingleOrDefault(x => x.State is false);
        if (shoppingSessionId is null)
        {
            return Result<ShoppingSession>.Failure(ShoppingSessionErrors.NotFound);
        }

        var shoppingSession = await GetShoppinsSessionById(shoppingSessionId.Id, cancellationToken);

        return shoppingSession.IsFailure
            ? Result<ShoppingSession>.Failure(shoppingSession.Error)
            : Result<ShoppingSession>.Success(shoppingSession.Value);
    }

    public async Task<Result<ShoppingSession>> DeleteCartItemFromCurrentShoppingSession(int shoppingSessionId, int cartItemId, CancellationToken cancellationToken)
    {
        var shoppingSession = await _shoppingSessionRepository.DeleteCartItemFromCurrentShoppingSession(shoppingSessionId, cartItemId, cancellationToken);

        return shoppingSession.IsFailure
            ? Result<ShoppingSession>.Failure(shoppingSession.Error)
            : Result<ShoppingSession>.Success(shoppingSession.Value);
    }

    public async Task<Result<ShoppingSession>> GetCurrentShoppingSession(CancellationToken cancellationToken)
    {
        var currentUserEmail = _currentUserService.GetClaim(ClaimTypes.Email);
        if (currentUserEmail.IsFailure)
        {
            return Result<ShoppingSession>.Failure(currentUserEmail.Error);
        }

        var currentUser = await _userService.GetUserByEmail(currentUserEmail.Value, cancellationToken);
        if (currentUser.IsFailure)
        {
            return Result<ShoppingSession>.Failure(currentUser.Error);
        }

        var currentShoppingSession = await GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(currentUser.Value, cancellationToken);
        
        return currentShoppingSession.IsFailure 
            ? Result<ShoppingSession>.Failure(currentShoppingSession.Error) 
            : Result<ShoppingSession>.Success(currentShoppingSession.Value);
    }

    public async Task<Result> UpdateOrDeleteCartItemInCurrentShoppingSession(int quantity, CartItem cartItem, ShoppingSession shoppingSession, CancellationToken cancellationToken)
    {
        if (quantity is 0)
        {
            var deleteCartItemFromShoppingSession = await DeleteCartItemFromCurrentShoppingSession(shoppingSession.Id, cartItem.Id, cancellationToken);
            var deleted = deleteCartItemFromShoppingSession.Value.CartItems.FirstOrDefault(cartItem);
            shoppingSession.TotalPrice -= deleted.Quantity * deleted.Article.Price;

            var updateShoppingSession = await UpdateShoppingSession(shoppingSession, cancellationToken);
            
            return updateShoppingSession.IsFailure 
                ? Result.Failure(updateShoppingSession.Error) 
                : Result.Success();
        }

        shoppingSession.TotalPrice -= cartItem.Quantity * cartItem.Article.Price;
        cartItem.Quantity = quantity;
        shoppingSession.TotalPrice += cartItem.Quantity * cartItem.Article.Price;
        
        var updateCartItem = await _cartItemService.Update(cartItem, cancellationToken);
        
        return updateCartItem.IsFailure 
            ? Result.Failure(updateCartItem.Error) 
            : Result.Success();
    }
}