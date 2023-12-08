using Backend.Utils;

namespace Backend.Services.Interfaces;

public interface IEncryptionService
{
    public string EncryptString(string plainText);
    public string DecryptString(string cipherText);
}
