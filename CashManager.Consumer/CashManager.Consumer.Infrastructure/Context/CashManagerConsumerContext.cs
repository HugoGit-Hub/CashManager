using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.Transactions;
using CashManager.Consumer.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Consumer.Infrastructure.Context;

public class CashManagerConsumerContext : DbContext
{
    public CashManagerConsumerContext(DbContextOptions<CashManagerConsumerContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = null!;

    public DbSet<Article> Articles { get; set; } = null!;

    public DbSet<Users> Users { get; set; } = null!;

    public DbSet<ShoppingSession> ShoppingSessions { get; set; } = null!;

    public DbSet<CartItem> CartItems { get; set; } = null!;
}
