using Backend.Base.Dal.Models;

namespace Backend.Auth.Dal.Models;

public class TeacherModel : BaseModel
{
    public required int UserId { get; set; }
    public UserModel? User { get; set; }
}
