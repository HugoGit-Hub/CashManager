using CashManager.Consumer.Application.Transactions;
using CashManager.Consumer.Domain.Transactions;
using CashManager.Consumer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Consumer.Infrastructure.Transactions;

internal class TransactionRepository : ITransactionRepository 
{ 
    private readonly CashManagerConsumerContext _context;

    public TransactionRepository(CashManagerConsumerContext context)
    {
        _context = context;
    }

    public async Task<Transaction> Create(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<Transaction> Put(Transaction transaction, CancellationToken cancellationToken)
    {
        var result = _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public Task<Transaction?> Get(Guid guid, CancellationToken cancellationToken)
    {
        return _context.Transactions.SingleOrDefaultAsync(t => t.Guid == guid, cancellationToken);
    }
}
