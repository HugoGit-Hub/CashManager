using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;
using MediatR;
using System.Security.Claims;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentShoppingSession;

internal class GetCurrentShoppingSessionQueryHandler : IRequestHandler<GetCurrentShoppingSessionQuery, Result<GetCurrentShoppingSessionResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;
    private readonly IShoppingSessionService _shoppingSessionService;

    public GetCurrentShoppingSessionQueryHandler(
        ICurrentUserService currentUserService,
        IUserService userService,
        IShoppingSessionService shoppingSessionService)
    {
        _currentUserService = currentUserService;
        _userService = userService;
        _shoppingSessionService = shoppingSessionService;
    }

    public async Task<Result<GetCurrentShoppingSessionResponse>> Handle(GetCurrentShoppingSessionQuery request, CancellationToken cancellationToken)
    {
        var currentUserEmail = _currentUserService.GetClaim(ClaimTypes.Email);
        if (currentUserEmail.IsFailure)
        {
            return Result<GetCurrentShoppingSessionResponse>.Failure(currentUserEmail.Error);
        }

        var currentUser = await _userService.GetUserByEmail(currentUserEmail.Value, cancellationToken);
        if (currentUser.IsFailure)
        {
            return Result<GetCurrentShoppingSessionResponse>.Failure(currentUser.Error);
        }

        var currentShoppingSession = await _shoppingSessionService.GetOrCreateShoppingSessionIfNullOrNotOpenShoppingSession(currentUser.Value, cancellationToken);
        if (currentShoppingSession.IsFailure)
        {
            return Result<GetCurrentShoppingSessionResponse>.Failure(currentShoppingSession.Error);
        }

        var getCurrentShoppingSessionResponse = new GetCurrentShoppingSessionResponse
        {
            TotalPrice = currentShoppingSession.Value.TotalPrice
        };

        return Result<GetCurrentShoppingSessionResponse>.Success(getCurrentShoppingSessionResponse);
    }
}