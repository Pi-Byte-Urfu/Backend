using Backend.Base.Dal.Models;

namespace Backend.Courses.Dal.Models;

public class StudentGroupsModel : BaseModel
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
}
