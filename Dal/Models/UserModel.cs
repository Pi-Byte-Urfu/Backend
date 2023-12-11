using Backend.Dal.Enums;

namespace Backend.Dal.Models;

public class UserModel : BaseModel
{
    public required string Email { get; set; }
    public required int HashedPassword { get; set; }
    public required UserType UserType { get; set; }
}

public class PasswordModel : BaseModel
{
    public required int HashedPassword { get; set; }
    public required string CryptedPassword { get; set; }
}