using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.User;

public interface IUserService
{
    public Task<Result<Users>> Get(CancellationToken cancellationToken);
}