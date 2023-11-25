namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionRepository
{
    public Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken);

    public Task<Transaction?> Get(int id, CancellationToken cancellationToken);

    public Task<Transaction> Update(Transaction transaction, CancellationToken cancellationToken);

    public Task<IEnumerable<Transaction>> GetPendingTransactionsForUser(int id, CancellationToken cancellationToken);
}