using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.Encryption;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Token;
using System.Text.Json;

namespace CashManager.Banking.Infrastructure.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly ITokenService _tokenService;
    private readonly IEncryptionService _encryptionService;

    public AuthenticationService(
        IUsersRepository usersRepository,
        IAuthenticationRepository authenticationRepository,
        ITokenService tokenService, 
        IEncryptionService encryptionService)
    {
        _usersRepository = usersRepository;
        _authenticationRepository = authenticationRepository;
        _tokenService = tokenService;
        _encryptionService = encryptionService;
    }

    public async Task<Result<string>> Login(string email, string password, CancellationToken cancellationToken)
    {
        var encryptedPassword = _encryptionService.Encrypt(password, email);
        var hashedPassword = _encryptionService.HashWithSalt(encryptedPassword);
        var result = await _authenticationRepository.IsCredentialsCorrects(email, hashedPassword, cancellationToken);
        if (!result)
        {
            return Result<string>.Failure(AuthenticationErrors.InvalidCredentialsError);
        }

        var user = await _usersRepository.Get(email, cancellationToken);
        var token = _tokenService.GenerateToken(user);
        var jsonToken = JsonSerializer.Serialize(token);

        return Result<string>.Success(jsonToken);
    }

    public async Task<Result<Users>> Register(Users user, CancellationToken cancellationToken)
    {
        var isUserUnique = await IsUsersEmailUnique(user.Email, cancellationToken);
        if (isUserUnique.IsFailure)
        {
            return Result<Users>.Failure(isUserUnique.Error);
        }

        var encryptedPassword = _encryptionService.Encrypt(user.Password, user.Email);
        var hashedPassword = _encryptionService.HashWithSalt(encryptedPassword);
        user.Password = hashedPassword;

        var postUser = await _usersRepository.Post(user, cancellationToken);

        return Result<Users>.Success(postUser);
    }

    private async Task<Result> IsUsersEmailUnique(string email, CancellationToken cancellationToken)
    {
        var result = await _authenticationRepository.IsUsersEmailUnique(email, cancellationToken);

        return result is not null
            ? Result.Failure(AuthenticationErrors.ExistingEmailError) 
            : Result.Success();
    }
}