using CashManager.Banking.Api.Controllers;
using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Application.Accounts.GetAccountByCurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CashManager.Banking.Test.Account;

public class AccountControllerShould
{
    private readonly Mock<ISender> _mockSender = new();

    [Fact]
    public async Task GetAccountsByCurrentUser_ReturnsOkResult_WhenQueryIsSuccessful()
    {
        // Arrange
        var expectedResult = Result<IEnumerable<AccountResponse>>.Success(It.IsAny<IEnumerable<AccountResponse>>());
        _mockSender
            .Setup(s => s.Send(It.IsAny<GetAccountsByCurrentUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        var sender = await _mockSender
            .Object
            .Send(It.IsAny<GetAccountsByCurrentUserQuery>(), It.IsAny<CancellationToken>());
        
        var controller = new AccountController(_mockSender.Object);

        // Act
        var result = await controller.GetAccountsByCurrentUser(It.IsAny<CancellationToken>());

        // Assert
        Assert.IsType<ActionResult<IEnumerable<AccountResponse>>>(result);
        Assert.Equal(expectedResult.Value, sender.Value);
    }

    [Fact]
    public async Task GetAccountsByCurrentUser_ReturnsBadRequest_WhenQueryFails()
    {
        // Arrange
        var errorResult = Result<IEnumerable<AccountResponse>>.Failure(It.IsAny<Error>());
        _mockSender
            .Setup(s => s.Send(It.IsAny<GetAccountsByCurrentUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorResult);

        var sender = await _mockSender
            .Object
            .Send(It.IsAny<GetAccountsByCurrentUserQuery>(), It.IsAny<CancellationToken>());
        
        var controller = new AccountController(_mockSender.Object);

        // Act
        var result = await controller.GetAccountsByCurrentUser(It.IsAny<CancellationToken>());

        // Assert
        Assert.IsType<ActionResult<IEnumerable<AccountResponse>>>(result);
        Assert.Equal(errorResult.Error, sender.Error);
    }
}