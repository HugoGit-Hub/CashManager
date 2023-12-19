using System.Security.Claims;
using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.User;
using MediatR;

namespace CashManager.Consumer.Application.ShoppingSessions.CreateShoppingSession;

internal class CreateShoppingSessionCommandHandler : IRequestHandler<CreateShoppingSessionCommand, Result>
{
    private readonly IShoppingSessionService _shoppingSessionService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserService _userService;

    public CreateShoppingSessionCommandHandler(
        IShoppingSessionService shoppingSessionService,
        ICurrentUserService currentUserService,
        IUserService userService)
    {
        _shoppingSessionService = shoppingSessionService;
        _currentUserService = currentUserService;
        _userService = userService;
    }

    public async Task<Result> Handle(CreateShoppingSessionCommand request, CancellationToken cancellationToken)
    {
        var emailCurrentUser = _currentUserService.GetClaim(ClaimTypes.Email);
        if (emailCurrentUser.IsFailure)
        {
            return Result.Failure(emailCurrentUser.Error);
        }
        
        var user = await _userService.GetUserByEmail(emailCurrentUser.Value, cancellationToken);
        if (emailCurrentUser.IsFailure)
        {
            return Result.Failure(user.Error);
        }

        var currentUserShoppingSessions = user.Value.ShoppingSessions.ToList();
        if (!currentUserShoppingSessions.All(x => x.State))
        {
            return Result.Success();
        }

        var shoppingSession = new ShoppingSession
        {
            State = false,
            TotalPrice = 0,
            UserId = user.Value.Id,
            User = user.Value
        };

        var createShoppingSession = await _shoppingSessionService.CreateShoppingSession(shoppingSession, cancellationToken);
        
        return createShoppingSession.IsFailure 
            ? Result.Failure(createShoppingSession.Error) 
            : Result.Success();
    }
}