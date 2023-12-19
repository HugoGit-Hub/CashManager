using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.Transactions;

namespace CashManager.Consumer.Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<Transaction>> Create(Transaction transaction, CancellationToken cancellationToken)
    {
        var create = await _transactionRepository.Create(transaction, cancellationToken);
        
        return Result<Transaction>.Success(create);
    }

    public async Task<Result<Transaction>> Put(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = await Get(transaction.Guid, cancellationToken);
        if (result.IsFailure)
        {
            return Result<Transaction>.Failure(result.Error);
        }

        result.Value.State = transaction.State;

        var put = await _transactionRepository.Put(result.Value, cancellationToken);
        
        return Result<Transaction>.Success(put);
    }

    public async Task<Result<Transaction>> Get(Guid guid, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.Get(guid, cancellationToken);

        return result == null
            ? Result<Transaction>.Failure(TransactionErrors.TransactionNotFound)
            : Result<Transaction>.Success(result);
    }
}
