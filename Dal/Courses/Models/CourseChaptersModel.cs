using Backend.Dal.Base.Models;

namespace Backend.Dal.Courses.Models;

public class CourseChaptersModel : BaseModel
{
    public string Name { get; set; }
    public int CourseId { get; set; }
}
