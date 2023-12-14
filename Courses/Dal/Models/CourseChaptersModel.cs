using Backend.Base.Dal.Models;

namespace Backend.Courses.Dal.Models;

public class CourseChaptersModel : BaseModel
{
    public string Name { get; set; }
    public int CourseId { get; set; }
}
