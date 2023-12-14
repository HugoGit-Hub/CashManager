using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.User;

public static class UserErrors
{
    public static Error EmailAlreadyExistError(string email) => new(
        "User.EmailAlreadyExist", $"The provided email : {email} already exist");

    public static readonly Error NotFoundError = new(
               "User.NotFound", "User with the provided email dosen't exist");
}