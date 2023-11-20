using CashManager.Consumer.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Consumer.Infrastructure.Context;

public class CashManagerConsumerContext : DbContext
{
    public CashManagerConsumerContext(DbContextOptions<CashManagerConsumerContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = null!;
}
