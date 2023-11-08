using CashManager.Banking.Domain.User;

namespace CashManager.Banking.Domain.Token;

public interface ITokenService
{
    public string GenerateToken(Users user);
}