namespace CashManager.Banking.Infrastructure.Encryption;

public interface IEncryptionService
{
    public string Encrypt(string data);

    public string HashWithSalt<TEntity>(TEntity data);
}