using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Domain.Authentication;

public interface IAuthenticationService
{
    public Task<string> Login(string email, string password, CancellationToken cancellationToken);

    public Task<string> Register(Users user, CancellationToken cancellationToken);
}