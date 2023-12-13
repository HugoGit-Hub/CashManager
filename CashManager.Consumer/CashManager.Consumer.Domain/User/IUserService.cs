using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.User;

public interface IUserService
{
    public Task<Result> IsEmailUnique(string email, CancellationToken cancellationToken);

    public Task Add(Users user, CancellationToken cancellationToken);
}