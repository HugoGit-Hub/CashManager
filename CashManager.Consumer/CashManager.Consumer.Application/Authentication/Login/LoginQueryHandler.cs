using CashManager.Consumer.Application.Security;
using CashManager.Consumer.Application.Token;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.User;
using MediatR;
using System.Text.Json;

namespace CashManager.Consumer.Application.Authentication.Login;

internal class LoginQueryHandler : IRequestHandler<LoginQuery, Result<string>>
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly ISecurityService _securityService;

    public LoginQueryHandler(
        IUserService userService,
        ITokenService tokenService, ISecurityService securityService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _securityService = securityService;
    }

    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var encryptPassword = _securityService.EncryptPassword(request.LoginRequest.Password, request.LoginRequest.Email);
        var hashWithSaltPassword = _securityService.HashWithSalt(encryptPassword);
        var areCredentialsCorrects = await _userService.AreCredentialsCorrects(request.LoginRequest.Email, hashWithSaltPassword, cancellationToken);
        if (areCredentialsCorrects.IsFailure)
        {
            return Result<string>.Failure(areCredentialsCorrects.Error);
        }

        var user = await _userService.GetUserByEmail(request.LoginRequest.Email, cancellationToken);
        if (user.IsFailure)
        {
            return Result<string>.Failure(user.Error);
        }

        var token = _tokenService.GenerateToken(user.Value);
        var jsonToken = JsonSerializer.Serialize(token);

        return Result<string>.Success(jsonToken);
    }
}