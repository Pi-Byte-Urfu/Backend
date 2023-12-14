using Backend.Dal.Base.Models;

namespace Backend.Dal.Courses.Models;

public class GroupCoursesModel : BaseModel
{
    public int GroupId { get; set; }
    public int CourseId { get; set; }
}
