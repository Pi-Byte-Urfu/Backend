namespace Backend.Models;

public class TeacherModel : BaseModel
{
    public required int AccountId { get; set; }
    public AccountModel? Account { get; set; }
}
