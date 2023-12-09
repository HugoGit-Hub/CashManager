using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;
using MediatR;
using System.Security.Claims;

namespace CashManager.Banking.Application.Accounts;

public class GetAccountsByCurrentUserQueryHandler : IRequestHandler<GetAccountsByCurrentUserQuery, Result<GetAccountsByCurrentUserResponse>>
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

    public async Task<Result<GetAccountsByCurrentUserResponse>> Handle(GetAccountsByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        if (email.IsFailure)
        {
            return Result<GetAccountsByCurrentUserResponse>.Failure(CurrentUserErrors.ClaimTypeNullError);
        }

        var user = await _usersRepository.Get(email.Value, cancellationToken);

        return Result<GetAccountsByCurrentUserResponse>.Success(new GetAccountsByCurrentUserResponse(user.Accounts));
    }
}