using Backend.Dal.Base.Models;

namespace Backend.Dal.Courses.Models;

public class CourseModel : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string CoursePhoto { get; set; }
    public int CreatorId { get; set; }
}
