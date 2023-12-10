using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Test.Helpers;
using FluentAssertions;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Test.Account;

public class AccountResponseShould
{
    private static readonly string ValidOwner = StringHelper.Random(50);
    private static readonly string ValidNumber = StringHelper.Random(34);
    private static readonly string InvalidOwner = StringHelper.Random(51);
    private static readonly string InvalidNumber = StringHelper.Random(35);

    [Fact]
    public void ReturnTrue_WithCorrectPropertyValues()
    {
        // Arrange
        var accountResponse = new AccountResponse
        {
            Id = It.IsAny<int>(),
            Owner = ValidOwner,
            Number = ValidNumber,
            Value = It.IsAny<double>(),
            OpenDateTime = DateTime.Now,
            CloseDateTime = DateTime.Now.AddDays(30)
        };

        // Act & Assert
        ValidateModel(accountResponse)
            .Should()
            .BeTrue();
    }

    [Fact]
    public void ReturnFalse_WithToLongOwnerPropertyValues()
    {
        var accountResponse = new AccountResponse
        {
            Id = It.IsAny<int>(),
            Owner = InvalidOwner,
            Number = ValidNumber,
            Value = It.IsAny<double>(),
            OpenDateTime = DateTime.Now,
            CloseDateTime = DateTime.Now.AddDays(30)
        };

        // Act & Assert
        ValidateModel(accountResponse)
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ReturnFalse_WithToLongNumberPropertyValues()
    {
        var accountResponse = new AccountResponse
        {
            Id = It.IsAny<int>(),
            Owner = ValidOwner,
            Number = InvalidNumber,
            Value = It.IsAny<double>(),
            OpenDateTime = DateTime.Now,
            CloseDateTime = DateTime.Now.AddDays(30)
        };

        // Act & Assert
        ValidateModel(accountResponse)
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ReturnFalse_WithEmptyPropertyValues()
    {
        var accountResponse = new AccountResponse();

        // Act & Assert
        ValidateModel(accountResponse)
            .Should()
            .BeFalse();
    }

    private static bool ValidateModel(AccountResponse accountResponse)
    {
        var validationContext = new ValidationContext(accountResponse, null, null);
        var validationResults = new List<ValidationResult>();

        return Validator.TryValidateObject(accountResponse, validationContext, validationResults, true);
    }
}