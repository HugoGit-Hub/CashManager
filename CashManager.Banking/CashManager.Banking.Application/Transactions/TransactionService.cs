using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.Encryption;
using CashManager.Banking.Domain.Transactions;
using System.Security.Claims;

namespace CashManager.Banking.Application.Transactions;

internal class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ICurrentUserService _currentUserService;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IEncryptionService encryptionService,
        ICurrentUserService currentUserService)
    {
        _transactionRepository = transactionRepository;
        _encryptionService = encryptionService;
        _currentUserService = currentUserService;
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

    public async Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken)
    {
        var id = _currentUserService.GetClaim(ClaimTypes.NameIdentifier);

        return await _transactionRepository.GetAll(Convert.ToInt32(id), cancellationToken);
    }
}