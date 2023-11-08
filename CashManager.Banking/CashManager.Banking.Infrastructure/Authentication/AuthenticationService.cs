using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.Token;
using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Infrastructure.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly ITokenService _tokenService;

    public AuthenticationService(
        IUsersRepository usersRepository,
        IAuthenticationRepository authenticationRepository,
        ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _authenticationRepository = authenticationRepository;
        _tokenService = tokenService;
    }

    public Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Register(Users user, CancellationToken cancellationToken)
    {
        var isUserUnique = await IsUsersEmailUnique(user.Email, cancellationToken);
        if (!isUserUnique)
        {
            throw new ExistingEmailException();
        }

        var result = await _usersRepository.Post(user, cancellationToken);
        
        return _tokenService.GenerateToken(result);
    }

    private async Task<bool> IsUsersEmailUnique(string email, CancellationToken cancellationToken)
    {
        var result = await _authenticationRepository.IsUsersEmailUnique(email, cancellationToken);
        return result is null;
    }
}