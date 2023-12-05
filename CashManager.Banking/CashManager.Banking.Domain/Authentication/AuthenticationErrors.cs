using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.Authentication;

public static class AuthenticationErrors
{
    public static readonly Error InvalidCredentialsError = new(
        "Authentication.InvalidCredentials", "Credentials are not corrects");

    public static readonly Error ExistingEmailError = new(
        "Authentication.ExistingEmail", "An user already exist with this email");
}