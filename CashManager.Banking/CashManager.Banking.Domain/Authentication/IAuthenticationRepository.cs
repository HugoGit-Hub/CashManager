using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Domain.Authentication;

public interface IAuthenticationRepository
{
    public Task<Users?> IsUsersEmailUnique(string email, CancellationToken cancellationToken);

    public Task<bool> IsCredentialsCorrects(string email, string password, CancellationToken cancellationToken);
}