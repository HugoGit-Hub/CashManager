namespace CashManager.Banking.Domain.Encryption;

public interface IEncryptionService
{
    public string Encrypt(string data, string userKey);

    public string HashWithSalt<TEntity>(TEntity data);
}