namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionService
{
    public Task<Transaction> SignAndPost(Transaction transaction, CancellationToken cancellationToken);

    public Task<IEnumerable<Transaction>> GetByUser(CancellationToken cancellationToken);

    public Task<Transaction> Validate(Transaction transaction, CancellationToken cancellationToken);
}