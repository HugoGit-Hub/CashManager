namespace CashManager.Banking.Domain.Token;

public interface ITokenService
{
    public string GenerateToken(string email);
}