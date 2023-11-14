﻿using CashManager.Banking.Domain.Accounts;

namespace CashManager.Banking.Application.Accounts;

internal class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task Transaction(string creditor, string debtor, double amount, CancellationToken cancellationToken)
    {
        var result = await CanAccountBeDebited(creditor, amount, cancellationToken);
        if (!result)
        {
            throw new NonDebatableAccountException();
        }

        var creditorAccount = await _accountRepository.Get(creditor, cancellationToken);
        var debtorAccount = await _accountRepository.Get(debtor, cancellationToken);
        creditorAccount.Value -= amount;
        debtorAccount.Value += amount;
        await _accountRepository.Update(creditorAccount, cancellationToken);
        await _accountRepository.Update(debtorAccount, cancellationToken);
    }

    private async Task<bool> CanAccountBeDebited(string number, double amount, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.Get(number, cancellationToken);
        
        return !(result.Value < amount);
    }
}