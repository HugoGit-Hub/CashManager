using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.Accounts;

public interface IAccountService
{
    public Task<Result> Transaction(string creditor, string debtor, double amount, CancellationToken cancellationToken);

    public Task<Result<IEnumerable<Account>>> Get(CancellationToken cancellationToken);
}