using CashManager.Consumer.Application.Security;
using CashManager.Consumer.Application.Token;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.User;
using MediatR;
using System.Text.Json;

namespace CashManager.Consumer.Application.Authentication.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly ISecurityService _securityService;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(
        ISecurityService securityService,
        IUserService userService,
        ITokenService tokenService)
    {
        _securityService = securityService;
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isemailUnique = await _userService.IsEmailUnique(request.RegisterRequest.Email, cancellationToken);
        if (isemailUnique.IsFailure)
        {
            return Result<string>.Failure(isemailUnique.Error);
        }

        var encruptedPassword = _securityService.EncryptPassword(request.RegisterRequest.Password, request.RegisterRequest.Email);
        var hasedAndSaltedPassword = _securityService.HashWithSalt(encruptedPassword);
        var user = new Users
        {
            Firstname = request.RegisterRequest.Firstname,
            Lastname = request.RegisterRequest.Lastname,
            Email = request.RegisterRequest.Email,
            Password = hasedAndSaltedPassword
        };
        await _userService.Add(user, cancellationToken);

        var token = _tokenService.GenerateToken(user);
        var jsonToken = JsonSerializer.Serialize(token);

        return Result<string>.Success(jsonToken);
    }
}