namespace Backend.Auth.Logic.Interfaces;

public interface IEncryptionService
{
    public string EncryptString(string plainText);
    public string DecryptString(string cipherText);
}
