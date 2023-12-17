using CashManager.Consumer.Domain.Transactions;

namespace CashManager.Consumer.Application.Transactions;

public interface ITransactionRepository
{
    public Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken);

    public Task<Transaction> Put(Transaction transaction, CancellationToken cancellationToken);

    public Task<Transaction?> Get(Guid guid, CancellationToken cancellationToken);
}
