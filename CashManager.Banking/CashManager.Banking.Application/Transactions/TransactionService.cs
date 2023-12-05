using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.Encryption;
using CashManager.Banking.Domain.ErrorHandling;
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

    public async Task<Result<Transaction>> SignAndPost(Transaction transaction, CancellationToken cancellationToken)
    {
        if (transaction.State != TransactionStateEnum.Pending)
        {
            return Result<Transaction>.Failure(TransactionErrors.WrongTransactionState(transaction.State));
        }
        
        var accountResult = await _accountRepository.Get(transaction.Creditor, cancellationToken);
        if (accountResult is null)
        {
            return Result<Transaction>.Failure(AccountError.AccountNotFoundError);
        }

        var user = await _usersRepository.GetByAccountNumber(transaction.Creditor, cancellationToken);
        var update = new Transaction
        {
            Id = 0,
            Creditor = transaction.Creditor,
            Debtor = transaction.Debtor,
            Type = transaction.Type,
            Amount = transaction.Amount,
            State = transaction.State,
            Date = transaction.Date,
            Guid = transaction.Guid,
            Url = transaction.Url,
            UserId = user.Id,
            User = user,
        };
        update.Signature = _encryptionService.HashWithSalt(update);

        var post = await _transactionRepository.Post(update, cancellationToken);

        return Result<Transaction>.Success(post);
    }

    public async Task<Result<IEnumerable<Transaction>>> GetByUserAccounts(string accountNumber, CancellationToken cancellationToken)
    {
        var emailResult = _currentUserService.GetClaim(ClaimTypes.Email);
        if (emailResult.IsFailure)
        {
            return Result<IEnumerable<Transaction>>.Failure(emailResult.Error);
        }

        var user = await _usersRepository.Get(emailResult.Value, cancellationToken);
        var transactions = user.Transactions
            .Where(t => t.Creditor == accountNumber || t.Debtor == accountNumber);

        return Result<IEnumerable<Transaction>>.Success(transactions);
    }

    public async Task<Result<IEnumerable<Transaction>>> GetPendingTransactionsForUser(CancellationToken cancellationToken)
    {
        var emailResult = _currentUserService.GetClaim(ClaimTypes.Email);
        if (emailResult.IsFailure)
        {
            return Result<IEnumerable<Transaction>>.Failure(emailResult.Error);
        }

        var user = await _usersRepository.Get(emailResult.Value, cancellationToken);
        var transactions = await _transactionRepository.GetPendingTransactionsForUser(user.Id, cancellationToken);

        return Result<IEnumerable<Transaction>>.Success(transactions);
    }

    public async Task<Result<Transaction>> ValidateOrAbort(
        Transaction transaction,
        TransactionStateEnum state,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetClaim(ClaimTypes.NameIdentifier);
        var storedTransaction = await _transactionRepository.Get(transaction.Id, cancellationToken);
        if (storedTransaction is null)
        {
            return Result<Transaction>.Failure(TransactionErrors.NotFoundTransactionError);
        }

        transaction.Id = 0;
        transaction.UserId = Convert.ToInt32(userId.Value);
        transaction.User = await _usersRepository.GetByAccountNumber(transaction.Creditor, cancellationToken);

        var transactionSignature = _encryptionService.HashWithSalt(transaction);
        if (transactionSignature != storedTransaction.Signature)
        {
            return Result<Transaction>.Failure(TransactionErrors.WrongSignatureError);
        }

        var updatedTransaction = new Transaction
        {
            Id = storedTransaction.Id,
            Creditor = transaction.Creditor,
            Debtor = transaction.Debtor,
            Type = transaction.Type,
            Amount = transaction.Amount,
            State = state,
            Date = transaction.Date,
            Guid = transaction.Guid,
            Signature = transactionSignature,
            Url = transaction.Url,
            UserId = transaction.UserId
        };

        var update = await _transactionRepository.Update(updatedTransaction, cancellationToken);

        return Result<Transaction>.Success(update);
    }
}