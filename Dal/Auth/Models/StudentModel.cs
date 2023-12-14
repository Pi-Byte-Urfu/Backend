using Backend.Dal.Base.Models;

namespace Backend.Dal.Auth.Models;

public class StudentModel : BaseModel
{
    public required int UserId { get; set; }
    public UserModel? User { get; set; }
}
