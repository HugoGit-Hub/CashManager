namespace CashManager.Banking.Domain.Accounts;

public interface IAccountRepository
{
    public Task<Account?> Get(string number, CancellationToken cancellationToken);

    public Task<Account> Update(Account account, CancellationToken cancellationToken);
    public Task<Account> Get(CancellationToken cancellationToken);
}