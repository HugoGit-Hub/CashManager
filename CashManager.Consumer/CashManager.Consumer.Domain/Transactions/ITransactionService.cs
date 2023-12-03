namespace CashManager.Consumer.Domain.Transactions;

public interface ITransactionService
{
    public Task<Transaction> Post(Transaction transaction,CancellationToken cancellationToken);

    public Task<Transaction> Put(Transaction transaction,CancellationToken cancellationToken); 

    public Task<Transaction> Get(Guid guid,CancellationToken cancellationToken);
}
