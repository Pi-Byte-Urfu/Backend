namespace Backend.Models;

public class UserModel
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required int HashedPassword { get; set; } 
}

public class PasswordModel
{
    public int Id { get; set; }
    public required int HashedPassword { get; set; }
    public required string CryptedPassword { get; set; }
}