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
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<Transaction?> Get(int id, CancellationToken cancellationToken)
    {
        return await _context.Transactions.FindAsync(id, cancellationToken);
    }

    public async Task<Transaction> Update(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}