namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionRepository
{
    public Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken);
    public Task<IEnumerable<Transaction>> GetAll(int id, CancellationToken cancellationToken);
}