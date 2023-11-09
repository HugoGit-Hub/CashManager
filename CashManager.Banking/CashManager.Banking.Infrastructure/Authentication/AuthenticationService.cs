﻿using System.Security.Authentication;
using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.Encryption;
using CashManager.Banking.Domain.Token;
using CashManager.Banking.Domain.User;

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
        ITokenService tokenService, IEncryptionService encryptionService)
    {
        _usersRepository = usersRepository;
        _authenticationRepository = authenticationRepository;
        _tokenService = tokenService;
        _encryptionService = encryptionService;
    }

    public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
    {
        var encryptedPassword = _encryptionService.Encrypt(password);
        var hashedPassword = _encryptionService.Hash(encryptedPassword);
        var result = await _authenticationRepository.IsCredentialsCorrects(email, hashedPassword, cancellationToken);
        if (!result)
        {
            throw new InvalidCredentialException();
        } 

        return _tokenService.GenerateToken(email);
    }

    public async Task<Users> Register(Users user, CancellationToken cancellationToken)
    {
        var isUserUnique = await IsUsersEmailUnique(user.Email, cancellationToken);
        if (!isUserUnique)
        {
            throw new ExistingEmailException();
        }

        var encryptedPassword = _encryptionService.Encrypt(user.Password);
        var hashedPassword = _encryptionService.Hash(encryptedPassword);
        user.Password = hashedPassword;

        return await _usersRepository.Post(user, cancellationToken);
    }

    private async Task<bool> IsUsersEmailUnique(string email, CancellationToken cancellationToken)
    {
        var result = await _authenticationRepository.IsUsersEmailUnique(email, cancellationToken);

        return result is null;
    }
}