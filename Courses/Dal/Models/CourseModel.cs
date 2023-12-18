using Backend.Base.Dal.Models;

namespace Backend.Courses.Dal.Models;

public class CourseModel : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string CoursePhoto { get; set; }
    public int CreatorId { get; set; }
}
