using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.Transactions;

public interface ITransactionService
{
    public Task<Result<Transaction>> Post(Transaction transaction,CancellationToken cancellationToken);

    public Task<Result<Transaction>> Put(Transaction transaction,CancellationToken cancellationToken); 

    public Task<Result<Transaction>> Get(Guid guid,CancellationToken cancellationToken);
}
