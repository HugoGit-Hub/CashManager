using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;
using FluentAssertions;
using Moq;
using System.Security.Claims;

namespace CashManager.Banking.Test;

public class GetAccountsByCurrentUserQueryHandlerHandleShould
{
    private readonly Mock<ICurrentUserService> _currentUserService = new();
    private readonly Mock<IUsersRepository> _usersRepository = new();

    [Fact]
    public async Task ReturnSuccessResult_WithValidUser()
    {
        // Arrange
        _currentUserService
            .Setup(c => c.GetClaim(ClaimTypes.Email))
            .Returns(Result<string>.Success("user@example.com"));

        var expectedUser = new Users();
        _usersRepository
            .Setup(u => u.Get("user@example.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUser);

        var query = new GetAccountsByCurrentUserQuery();
        var handler = new GetAccountsByCurrentUserQueryHandler(_currentUserService.Object, _usersRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result
            .Should()
            .BeOfType<Result<GetAccountsByCurrentUserResponse>>();

        result
            .IsSuccess
            .Should()
            .BeTrue();

        result
            .Should()
            .NotBeNull();

        result
            .Value
            .Should()
            .NotBeNull();

        result
            .Value
            .Accounts
            .Should()
            .BeEquivalentTo(expectedUser.Accounts);
    }

    [Fact]
    public async Task ReturnFailureResult_WhenClaimTypeIsNull()
    {
        // Arrange
        _currentUserService
            .Setup(c => c.GetClaim(ClaimTypes.Email))
            .Returns(Result<string>.Failure(CurrentUserErrors.ClaimTypeNullError));

        var query = new GetAccountsByCurrentUserQuery();
        var handler = new GetAccountsByCurrentUserQueryHandler(_currentUserService.Object, _usersRepository.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result
            .Should()
            .BeOfType<Result<GetAccountsByCurrentUserResponse>>();
        
        result
            .IsFailure
            .Should()
            .BeTrue();
        
        result
            .Error
            .Should()
            .Be(CurrentUserErrors.ClaimTypeNullError);
    }
}