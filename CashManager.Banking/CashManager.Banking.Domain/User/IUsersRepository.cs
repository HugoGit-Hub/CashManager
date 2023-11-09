namespace CashManager.Banking.Domain.User;

public interface IUsersRepository
{
    public Task<Users> Post(Users user, CancellationToken cancellationToken);
}