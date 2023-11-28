using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.CurrentUser;
using System.Security.Claims;
using CashManager.Banking.Domain.User;

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

    public async Task Transaction(string creditor, string debtor, double amount, CancellationToken cancellationToken)
    {
        var creditorAccount = await _accountRepository.Get(creditor, cancellationToken) ?? throw new NullAccountException();
        var debtorAccount = await _accountRepository.Get(debtor, cancellationToken) ?? throw new NullAccountException();
        var result = await CanAccountBeDebited(creditor, amount, cancellationToken);
        if (!result)
        {
            throw new NonDebatableAccountException();
        }

        creditorAccount.Value -= amount;
        debtorAccount.Value += amount;
        await _accountRepository.Update(creditorAccount, cancellationToken);
        await _accountRepository.Update(debtorAccount, cancellationToken);
    }

    public async Task<IEnumerable<Account>> Get(CancellationToken cancellationToken)
    {
        var email = _currentUserService.GetClaim(ClaimTypes.Email);
        var result = await _usersRepository.Get(email, cancellationToken);

        return result.Accounts;
    }

    private async Task<bool> CanAccountBeDebited(string number, double amount, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.Get(number, cancellationToken) ?? throw new NullAccountException();
        
        return !(result.Value < amount);
    }
}