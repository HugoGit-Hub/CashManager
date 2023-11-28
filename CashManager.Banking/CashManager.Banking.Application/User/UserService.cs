using CashManager.Banking.Domain.CurrentUser;
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

    public async Task<Users> Get(CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email); 

        return await _usersRepository.Get(email, cancellationToken);
    }
}