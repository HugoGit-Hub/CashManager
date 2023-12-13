using CashManager.Consumer.Application.User;
using CashManager.Consumer.Domain.User;
using CashManager.Consumer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Consumer.Infrastructure.User;

internal class UserRepository : IUserRepository
{
    private readonly CashManagerConsumerContext _context;

    public UserRepository(CashManagerConsumerContext context)
    {
        _context = context;
    }

    public Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task Add(Users user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}