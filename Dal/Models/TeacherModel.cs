namespace Backend.Dal.Models;

public class TeacherModel : BaseModel
{
    public required int UserId { get; set; }
    public UserModel? User { get; set; }
}
