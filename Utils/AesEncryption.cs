using System.Security.Cryptography;
using System.Text;

namespace Backend.Utils;

public class AesSettings
{
    public byte[] Key {  get; set; }
    public byte[] IV { get; set; }
}

public class AESEncryption
{
    public static AesSettings GetAesSettingsFromConfig(IConfiguration configuration)
    {
        return new AesSettings()
        {
            Key = Convert.FromBase64String(configuration.GetValue(typeof(string), "EncryptionKey")?.ToString()),
            IV = Convert.FromBase64String(configuration.GetValue(typeof(string), "EncryptionIV")?.ToString()),
        };
    }

    public static string EncryptString(AesSettings settings, string plainText)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = settings.Key;
        aesAlg.IV = settings.IV;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();

        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string DecryptString(AesSettings settings, string cipherText)
    {
        var cipherBytes = Convert.FromBase64String(cipherText);

        using var aesAlg = Aes.Create();
        aesAlg.Key = settings.Key;
        aesAlg.IV = settings.IV;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }
}