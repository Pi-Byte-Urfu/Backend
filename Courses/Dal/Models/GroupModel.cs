using Backend.Auth.Dal.Models;
using Backend.Base.Dal.Models;

namespace Backend.Courses.Dal.Models;

public class GroupModel : BaseModel
{
    public required string GroupName { get; set; }
    public required string AddGuid { get; set; }

    public required int TeacherId { get; set; }
    public TeacherModel Teacher { get; set; }
}