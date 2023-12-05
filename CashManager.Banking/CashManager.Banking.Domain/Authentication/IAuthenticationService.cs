using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Domain.Authentication;

public interface IAuthenticationService
{
    public Task<Result<string>> Login(string email, string password, CancellationToken cancellationToken);

    public Task<Result<Users>> Register(Users user, CancellationToken cancellationToken);
}