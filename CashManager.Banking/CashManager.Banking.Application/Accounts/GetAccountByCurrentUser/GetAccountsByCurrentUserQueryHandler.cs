using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;
using Mapster;
using MediatR;
using System.Security.Claims;

namespace CashManager.Banking.Application.Accounts.GetAccountByCurrentUser;

public class GetAccountsByCurrentUserQueryHandler : IRequestHandler<GetAccountsByCurrentUserQuery, Result<IEnumerable<AccountResponse>>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;

    public GetAccountsByCurrentUserQueryHandler(
        ICurrentUserService currentUserService,
        IUsersRepository usersRepository)
    {
        _currentUserService = currentUserService;
        _usersRepository = usersRepository;
    }

    public async Task<Result<IEnumerable<AccountResponse>>> Handle(GetAccountsByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        if (email.IsFailure)
        {
            return Result<IEnumerable<AccountResponse>>.Failure(CurrentUserErrors.ClaimTypeNullError);
        }

        var user = await _usersRepository.Get(email.Value, cancellationToken);

        return Result<IEnumerable<AccountResponse>>
            .Success(user.Accounts.Adapt<IEnumerable<AccountResponse>>());
    }
}