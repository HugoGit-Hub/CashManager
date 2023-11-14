namespace CashManager.Banking.Domain.User;

public interface IUsersRepository
{
    public Task<Users> Post(Users user, CancellationToken cancellationToken);

    public Task<Users> Get(string email, CancellationToken cancellationToken);

    public Task<Users> GetByAccountNumber(string accountNumber, CancellationToken cancellationToken);
}