namespace Backend.Logic.Auth.Interfaces;

public interface IEncryptionService
{
    public string EncryptString(string plainText);
    public string DecryptString(string cipherText);
}
