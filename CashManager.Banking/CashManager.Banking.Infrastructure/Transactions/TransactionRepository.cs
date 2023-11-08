using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Infrastructure.Context;

namespace CashManager.Banking.Infrastructure.Transactions;

internal class TransactionRepository : ITransactionRepository
{
    private readonly CashManagerBankingContext _context;

    public TransactionRepository(CashManagerBankingContext context)
    {
        _context = context;
    }

    public async Task<Transaction> Post(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = await _context.Transactions.AddAsync(transaction, cancellationToken);
        return result.Entity;
    }
}