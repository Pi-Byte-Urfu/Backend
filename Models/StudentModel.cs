namespace Backend.Models;

public class StudentModel : BaseModel
{
    public required int AccountId { get; set; }
    public AccountModel? Account { get; set; }
}
