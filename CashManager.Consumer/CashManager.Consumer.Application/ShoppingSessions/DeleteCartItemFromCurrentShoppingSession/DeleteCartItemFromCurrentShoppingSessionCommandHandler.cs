using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;
using MediatR;
using System.Security.Claims;

namespace CashManager.Consumer.Application.ShoppingSessions.DeleteCartItemFromCurrentShoppingSession;

internal class DeleteCartItemFromCurrentShoppingSessionCommandHandler : IRequestHandler<DeleteCartItemFromCurrentShoppingSessionCommand, Result>
{
    private readonly IShoppingSessionService _shoppingSessionService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public DeleteCartItemFromCurrentShoppingSessionCommandHandler(
        IShoppingSessionService shoppingSessionService,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _shoppingSessionService = shoppingSessionService;
        _currentUserService = currentUserService;
        _userService = userService;
    }

    public async Task<Result> Handle(DeleteCartItemFromCurrentShoppingSessionCommand request, CancellationToken cancellationToken)
    {
        var currentUserEmail = _currentUserService.GetClaim(ClaimTypes.Email);
        if (currentUserEmail.IsFailure)
        {
            return Result.Failure(currentUserEmail.Error);
        }

        var currentUser = await _userService.GetUserByEmail(currentUserEmail.Value, cancellationToken);
        if (currentUser.IsFailure)
        {
            return Result.Failure(currentUser.Error);
        }

        var currentShoppingSession = await _shoppingSessionService.GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(currentUser.Value, cancellationToken);
        if (currentShoppingSession.IsFailure)
        {
            return Result.Failure(currentShoppingSession.Error);
        }

        var deleteCartItemFromShoppingSession = await _shoppingSessionService.DeleteCartItemFromCurrentShoppingSession(currentShoppingSession.Value.Id, request.CartItemId, cancellationToken);

        return deleteCartItemFromShoppingSession.IsFailure
            ? Result.Failure(deleteCartItemFromShoppingSession.Error)
            : Result.Success();
    }
}