using Backend.Enums;

namespace Backend.DataTransferObjects;

public class AccountCreatingDto : Dto
{
    public int UserId { get; set; }
    public UserType UserType { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
