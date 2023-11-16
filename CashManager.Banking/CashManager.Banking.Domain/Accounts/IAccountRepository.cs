namespace CashManager.Banking.Domain.Accounts;

public interface IAccountRepository
{
    public Task<Account?> Get(string number, CancellationToken cancellationToken);

    public Task<Account?> Get(int id, CancellationToken cancellationToken);
    
    public Task<Account> Update(Account account, CancellationToken cancellationToken);
}