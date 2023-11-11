namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionRepository
{
    public Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken);
}