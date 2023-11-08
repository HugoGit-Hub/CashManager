namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionService
{
    public Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken);
}