using System.ComponentModel;

namespace Backend.Dal.Models;

public class AccountModel : BaseModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }

    public required string PhotoUrl { get; set; }

    public required int UserId { get; set; }
}
