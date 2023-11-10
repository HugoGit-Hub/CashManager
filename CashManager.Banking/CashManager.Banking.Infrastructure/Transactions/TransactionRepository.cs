using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Transaction>> GetAll(int userId, CancellationToken cancellationToken)
    {
        var result = await _context.Transactions.Where(Transaction => Transaction.UserId == userId).ToListAsync(cancellationToken);
       return result;
    }
}