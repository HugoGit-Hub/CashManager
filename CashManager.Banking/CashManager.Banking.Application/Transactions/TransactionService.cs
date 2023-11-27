using CashManager.Banking.Domain.Accounts;
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
    private readonly IAccountRepository _accountRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ICurrentUserService _currentUserService;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IUsersRepository usersRepository,
        IAccountRepository accountRepository,
        IEncryptionService encryptionService,
        ICurrentUserService currentUserService)
    {
        _transactionRepository = transactionRepository;
        _usersRepository = usersRepository;
        _accountRepository = accountRepository;
        _encryptionService = encryptionService;
        _currentUserService = currentUserService;
    }

    public async Task<Transaction> SignAndPost(Transaction transaction, CancellationToken cancellationToken)
    {
        if (transaction.State != TransactionStateEnum.Pending)
        {
            throw new BadTransactionStateException($"Bad transaction state : {transaction.State}");
        }
        
        var account = await _accountRepository.Get(transaction.Creditor, cancellationToken);
        if (account is null)
        {
            throw new UserAccountNotFoundException("No account found for this user");
        }
        
        var user = await _usersRepository.GetByAccountNumber(transaction.Creditor, cancellationToken);
        transaction.UserId = user.Id;
        transaction.Guid = Guid.NewGuid();

        var transactionSignature = _encryptionService.HashWithSalt(transaction);
        transaction.Signature = transactionSignature;

        return await _transactionRepository.Post(transaction, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByUserAccounts(string accountNumber, CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        var user = await _usersRepository.Get(email, cancellationToken);

        return user.Transactions
            .Where(t => t.Creditor == accountNumber || t.Debtor == accountNumber);
    }

    public async Task<Transaction> Validate(Transaction transaction, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetClaim(ClaimTypes.NameIdentifier);
        transaction.UserId = Convert.ToInt32(userId);

        var storedTransaction = await _transactionRepository.Get(transaction.Id, cancellationToken) ?? throw new NullTransactionException();
        transaction.Id = 0;

        var transactionSignature = _encryptionService.HashWithSalt(transaction);
        if (transactionSignature != storedTransaction.Signature)
        {
            throw new WrongSignatureException();
        }

        var updatedTransaction = new Transaction
        {
            Id = storedTransaction.Id,
            Creditor = transaction.Creditor,
            Debtor = transaction.Debtor,
            Type = transaction.Type,
            Amount = transaction.Amount,
            State = TransactionStateEnum.Success,
            Date = transaction.Date,
            Guid = transaction.Guid,
            Signature = transactionSignature,
            Url = transaction.Url,
            UserId = transaction.UserId
        };

        return await _transactionRepository.Update(updatedTransaction, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetPendingTransactionsForUser(CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        var user = await _usersRepository.Get(email, cancellationToken);

        return await _transactionRepository.GetPendingTransactionsForUser(user.Id, cancellationToken);
    }

    public async Task<Transaction> Abort(Transaction transaction, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetClaim(ClaimTypes.NameIdentifier);
        transaction.UserId = Convert.ToInt32(userId);

        var storedTransaction = await _transactionRepository.Get(transaction.Id, cancellationToken) ?? throw new NullTransactionException();
        transaction.Id = 0;

        var transactionSignature = _encryptionService.HashWithSalt(transaction);
        if (transactionSignature != storedTransaction.Signature)
        {
            throw new WrongSignatureException();
        }

        var updatedTransaction = new Transaction
        {
            Id = storedTransaction.Id,
            Creditor = transaction.Creditor,
            Debtor = transaction.Debtor,
            Type = transaction.Type,
            Amount = transaction.Amount,
            State = TransactionStateEnum.Aborted,
            Date = transaction.Date,
            Guid = transaction.Guid,
            Signature = transactionSignature,
            Url = transaction.Url,
            UserId = transaction.UserId
        };

        return await _transactionRepository.Update(updatedTransaction, cancellationToken);
    }
}