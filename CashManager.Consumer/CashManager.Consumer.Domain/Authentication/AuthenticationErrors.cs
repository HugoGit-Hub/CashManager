using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.Authentication;

public static class AuthenticationErrors
{
    public static readonly Error WrongCredentialsError = new(
        "Authentication.WrongCredentials", "Provided email and password doesn't match");
}