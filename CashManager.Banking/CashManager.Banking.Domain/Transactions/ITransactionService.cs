using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Domain.Transactions;

public interface ITransactionService
{
    public Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken);

    public Task<IEnumerable<Transaction>> GetAll(int userId,CancellationToken cancellationToken);
}