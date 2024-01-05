using System.Security.Cryptography;
using Backend.Auth.Logic.Interfaces;

namespace Backend.Auth.Logic;

/// <summary>
/// Provides encryption and decryption services using the AES algorithm.
/// </summary>
public class EncryptionService : IEncryptionService
{
    private byte[] _key { get; set; }
    private byte[] _iv { get; set; }

    /// <summary>
    /// Initializes a new instance of the EncryptionService class with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration object containing the encryption settings.</param>
    public EncryptionService(IConfiguration configuration)
    {
        var aesSettings = GetAesSettingsFromConfig(configuration);
        _key = aesSettings.Key;
        _iv = aesSettings.IV;
    }

    private static AesSettings GetAesSettingsFromConfig(IConfiguration configuration)
    {
        return new AesSettings()
        {
            Key = Convert.FromBase64String(configuration.GetValue(typeof(string), "EncryptionKey")?.ToString()),
            IV = Convert.FromBase64String(configuration.GetValue(typeof(string), "EncryptionIV")?.ToString()),
        };
    }

    /// <summary>
    /// Encrypts the specified plain text using the AES algorithm.
    /// </summary>
    /// <param name="plainText">The plain text to encrypt.</param>
    /// <returns>The encrypted cipher text.</returns>
    public string EncryptString(string plainText)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();

        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    /// <summary>
    /// Decrypts the specified cipher text using the AES algorithm.
    /// </summary>
    /// <param name="cipherText">The cipher text to decrypt.</param>
    /// <returns>The decrypted plain text.</returns>
    public string DecryptString(string cipherText)
    {
        var cipherBytes = Convert.FromBase64String(cipherText);

        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }

    internal class AesSettings
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
