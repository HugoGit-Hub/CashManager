using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Banking.Infrastructure.Authentication;

internal class AuthenticationRepository : IAuthenticationRepository
{
    private readonly CashManagerBankingContext _context;

    public AuthenticationRepository(CashManagerBankingContext context)
    {
        _context = context;
    }

    public async Task<Users?> IsUsersEmailUnique(string email, CancellationToken cancellationToken)
    {
        var result = await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        return result;
    }

    public async Task<bool> IsCredentialsCorrects(string email, string password, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(u => u.Email == email && u.Password == password, cancellationToken);
    }
}