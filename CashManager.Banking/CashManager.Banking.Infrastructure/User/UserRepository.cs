using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Context;

namespace CashManager.Banking.Infrastructure.User;

public class UserRepository : IUsersRepository
{
    private readonly CashManagerBankingContext _context;

    public UserRepository(CashManagerBankingContext context)
    {
        _context = context;
    }

    public async Task<Users> Post(Users user, CancellationToken cancellationToken)
    {
        var result = await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
}