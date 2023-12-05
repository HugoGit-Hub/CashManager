using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionService
{
    public Task<Result<Transaction>> SignAndPost(Transaction transaction, CancellationToken cancellationToken);

    public Task<Result<IEnumerable<Transaction>>> GetByUserAccounts(string accountNumber, CancellationToken cancellationToken);

    public Task<Result<IEnumerable<Transaction>>> GetPendingTransactionsForUser(CancellationToken cancellationToken);

    public Task<Result<Transaction>> ValidateOrAbort(Transaction transaction, TransactionStateEnum state, CancellationToken cancellationToken);
}