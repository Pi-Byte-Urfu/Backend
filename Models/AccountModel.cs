using Backend.Enums;

namespace Backend.Models;

public class AccountModel : BaseModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? PhotoUrl { get; set; }

    public required int UserId { get; set; }
    //public UserModel? User { get; set; }
}
