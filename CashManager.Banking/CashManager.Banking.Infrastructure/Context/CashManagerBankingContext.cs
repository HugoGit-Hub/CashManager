using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Banking.Infrastructure.Context;

public class CashManagerBankingContext : DbContext
{
    public CashManagerBankingContext(DbContextOptions<CashManagerBankingContext> options)
        : base(options)
    {
    }

    public DbSet<Account>? Accounts { get; set; }

    public DbSet<Transaction>? Transactions { get; set; }

    public DbSet<Users>? Users { get; set; }
}