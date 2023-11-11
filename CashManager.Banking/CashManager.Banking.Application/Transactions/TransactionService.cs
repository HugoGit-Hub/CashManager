using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.Encryption;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Domain.User;
using System.Security.Claims;

namespace CashManager.Banking.Application.Transactions;

internal class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ICurrentUserService _currentUserService;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IUsersRepository usersRepository,
        IEncryptionService encryptionService,
        ICurrentUserService currentUserService)
    {
        _transactionRepository = transactionRepository;
        _encryptionService = encryptionService;
        _currentUserService = currentUserService;
        _usersRepository = usersRepository;
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

    public async Task<IEnumerable<Transaction>> GetByUser(CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        var user = await _usersRepository.Get(email, cancellationToken);

        return user.Transactions;
    }
}