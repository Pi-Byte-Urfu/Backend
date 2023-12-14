using Backend.Base.Dal.Models;

namespace Backend.Courses.Dal.Models;

public class GroupCoursesModel : BaseModel
{
    public int GroupId { get; set; }
    public int CourseId { get; set; }
}
