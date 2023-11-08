using CashManager.Banking.Domain.Encryption;

namespace CashManager.Banking.Infrastructure.Encryption;

internal class EncryptionService : IEncryptionService
{
    public byte[] Encrypt(string data)
    {
        throw new NotImplementedException();
    }

    public string Decrypt(byte[] data)
    {
        throw new NotImplementedException();
    }

    public string Hash(string data)
    {
        throw new NotImplementedException();
    }

    public string HashWithSalt(string data, string salt)
    {
        throw new NotImplementedException();
    }

    public string GenereateSalt()
    {
        throw new NotImplementedException();
    }
}