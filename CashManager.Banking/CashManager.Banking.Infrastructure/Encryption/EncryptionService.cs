﻿using CashManager.Banking.Domain.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace CashManager.Banking.Infrastructure.Encryption;

internal class EncryptionService : IEncryptionService
{
    private const string EncodingKey = "b14ca5898a4e4133bbce2ea2315a1916"; //TODO: Replace this hard coded encoding key
    private const string Salt = "0x055E1093465E94F89D15BECD09BD2508A011D67CF067259CD78CD9CC949C8585"; //TODO: Replace this hard coded salt

    public string Encrypt(string data)
    {
        var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(EncodingKey);
        aes.IV = new byte[16];

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(data);
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Hash(string data)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(data);
        var saltBytes = Encoding.UTF8.GetBytes(Salt);
        var combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

        using var sha512 = SHA512.Create();
        var hashBytes = sha512.ComputeHash(combinedBytes);
        return Convert.ToBase64String(hashBytes);
    }
}