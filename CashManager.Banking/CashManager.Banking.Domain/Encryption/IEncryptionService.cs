namespace CashManager.Banking.Domain.Encryption;

public interface IEncryptionService
{
    public byte[] Encrypt(string data);

    public string Decrypt(byte[] data);

    public string Hash(string data);

    public string HashWithSalt(string data, string salt);

    public string GenereateSalt();
}