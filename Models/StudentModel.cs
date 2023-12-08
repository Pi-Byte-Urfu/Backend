namespace Backend.Models;

public class StudentModel : BaseModel
{
    public required int UserId { get; set; }
    public UserModel? User { get; set; }
}
