using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Banking.Infrastructure.User;

public class UsersRepository : IUsersRepository
{
    private readonly CashManagerBankingContext _context;

    public UsersRepository(CashManagerBankingContext context)
    {
        _context = context;
    }

    public async Task<Users> Post(Users user, CancellationToken cancellationToken)
    {
        var result = await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<Users> Get(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Transactions)
            .Include(u => u.Accounts)
            .FirstAsync(u => u.Email == email, cancellationToken);
    }
}