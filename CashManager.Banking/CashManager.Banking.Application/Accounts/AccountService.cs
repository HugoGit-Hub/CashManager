using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.User;
using System.Security.Claims;

namespace CashManager.Banking.Application.Accounts;

internal class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ICurrentUserService _currentUserService;

    public AccountService(
        IAccountRepository accountRepository,
        IUsersRepository usersRepository,
        ICurrentUserService currentUserService)
    {
        _accountRepository = accountRepository;
        _currentUserService = currentUserService;
        _usersRepository = usersRepository;
    }

    public async Task<Result> CreditAndDebit(string creditor, string debtor, double amount, CancellationToken cancellationToken)
    {
        var creditorAccount = await _accountRepository.Get(creditor, cancellationToken);
        var debtorAccount = await _accountRepository.Get(debtor, cancellationToken);
        if (creditorAccount is null || debtorAccount is null)
        {
            return Result.Failure(AccountError.AccountNotFoundError);
        }

        var result = await CanAccountBeDebited(creditor, amount, cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        creditorAccount.Value -= amount;
        debtorAccount.Value += amount;
        await _accountRepository.Update(creditorAccount, cancellationToken);
        await _accountRepository.Update(debtorAccount, cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<Account>>> Get(CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        if (email.IsFailure)
        {
            return Result<IEnumerable<Account>>.Failure(CurrentUserErrors.ClaimTypeNullError);
        }

        var user = await _usersRepository.Get(email.Value, cancellationToken);

        return Result<IEnumerable<Account>>.Success(user.Accounts);
    }

    private async Task<Result> CanAccountBeDebited(string number, double amount, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.Get(number, cancellationToken);
        if (result is null)
        {
            return Result.Failure(AccountError.AccountNotFoundError);
        }

        var isDebitable = !(result.Value < amount);

        return !isDebitable ? Result.Failure(AccountError.NonDebatableAccountError(number, amount)) : Result.Success();
    }
}