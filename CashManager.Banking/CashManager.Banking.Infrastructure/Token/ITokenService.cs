using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Infrastructure.Token;

public interface ITokenService
{
    public string GenerateToken(Users user);
}