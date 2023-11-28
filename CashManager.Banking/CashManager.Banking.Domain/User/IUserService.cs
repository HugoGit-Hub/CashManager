namespace CashManager.Banking.Domain.User;

public interface IUserService
{
    public Task<Users> Get(CancellationToken cancellationToken);
}