using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;
using System.Security.Claims;

namespace CashManager.Banking.Application.User;

internal class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ICurrentUserService _currentUserService;

    public UserService(
        IUsersRepository usersRepository, 
        ICurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Users>> Get(CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        if (email.IsFailure)
        {
            return Result<Users>.Failure(CurrentUserErrors.ClaimTypeNullError);
        }

        var user = await _usersRepository.Get(email.Value, cancellationToken);

        return Result<Users>.Success(user);
    }
}