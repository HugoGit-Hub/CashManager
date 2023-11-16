using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.CurrentUser;
using System.Security.Claims;

namespace CashManager.Banking.Application.Accounts;

internal class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentUserService _currentUserService;

    public AccountService(
        IAccountRepository accountRepository,
        ICurrentUserService currentUserService)
    {
        _accountRepository = accountRepository;
        _currentUserService = currentUserService;
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

    private async Task<bool> CanAccountBeDebited(string number, double amount, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.Get(number, cancellationToken) ?? throw new NullAccountException();
        
        return !(result.Value < amount);
    }

    public async Task<Account> Get(CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetClaim(ClaimTypes.NameIdentifier);
        var result = await _accountRepository.Get(Convert.ToInt32(userId), cancellationToken) ?? throw new NullAccountException();

        return result;
    }
}