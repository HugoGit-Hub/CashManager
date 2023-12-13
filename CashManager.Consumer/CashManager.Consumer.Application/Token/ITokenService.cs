using CashManager.Consumer.Domain.User;

namespace CashManager.Consumer.Application.Token;

public interface ITokenService
{
    public string GenerateToken(Users user);
}