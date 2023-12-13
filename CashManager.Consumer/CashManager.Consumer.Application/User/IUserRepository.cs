using CashManager.Consumer.Domain.User;

namespace CashManager.Consumer.Application.User;

public interface IUserRepository
{
    public Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);

    public Task Add(Users user, CancellationToken cancellationToken);
}