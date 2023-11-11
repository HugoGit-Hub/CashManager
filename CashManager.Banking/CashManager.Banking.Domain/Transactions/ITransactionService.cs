namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionService
{
    public Task<Transaction> SignAndPost(Transaction transaction, CancellationToken cancellationToken);
}