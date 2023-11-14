using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Banking.Infrastructure.Accounts;

internal class AccountRepository : IAccountRepository
{
    private readonly CashManagerBankingContext _context;

    public AccountRepository(CashManagerBankingContext context)
    {
        _context = context;
    }

    public Task<Account> Get(string number, CancellationToken cancellationToken)
    {
        return _context.Accounts.FirstAsync(a => a.Number == number, cancellationToken);
    }

    public async Task<Account> Update(Account account, CancellationToken cancellationToken)
    {
        var result = _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}