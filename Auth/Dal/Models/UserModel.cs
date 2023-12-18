using Backend.Auth.Enums;
using Backend.Base.Dal.Models;

namespace Backend.Auth.Dal.Models;

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