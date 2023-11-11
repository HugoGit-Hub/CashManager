using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Infrastructure.Encryption;

namespace CashManager.Banking.Application.Transactions;

internal class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEncryptionService _encryptionService;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IEncryptionService encryptionService)
    {
        _transactionRepository = transactionRepository;
        _encryptionService = encryptionService;
    }

    public async Task<Transaction> SignAndPost(Transaction transaction, CancellationToken cancellationToken)
    {
        if (transaction.State != TransactionStateEnum.Pending)
        {
            throw new BadTransactionStateException($"Bad transaction state : {transaction.State}");
        }

        var transactionSignature = _encryptionService.HashWithSalt(transaction);
        transaction.Signature = transactionSignature;

        return await _transactionRepository.Post(transaction, cancellationToken);
    }
}