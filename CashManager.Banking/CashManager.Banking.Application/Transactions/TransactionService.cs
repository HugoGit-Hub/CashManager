using CashManager.Banking.Domain.Transactions;

namespace CashManager.Banking.Application.Transactions;

internal class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken)
    {
        return await _transactionRepository.Post(transaction, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetAll(int userId, CancellationToken cancellationToken)
    {
        return await _transactionRepository.GetAll(userId, cancellationToken);
    }
}