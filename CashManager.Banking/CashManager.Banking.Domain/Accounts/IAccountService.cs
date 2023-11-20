﻿namespace CashManager.Banking.Domain.Accounts;

public interface IAccountService
{
    public Task Transaction(string creditor, string debtor, double amount, CancellationToken cancellationToken);

    public Task<IEnumerable<Account>> Get(CancellationToken cancellationToken);
}