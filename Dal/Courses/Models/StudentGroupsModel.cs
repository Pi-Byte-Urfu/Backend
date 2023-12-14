using Backend.Dal.Base.Models;

namespace Backend.Dal.Courses.Models;

public class StudentGroupsModel : BaseModel
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
}
