using Backend.Base.Dal.Models;
using Backend.CoursePages.Enums;

namespace Backend.CoursePages.Dal.Models;

public class CoursePageModel : BaseModel
{
    public int ChapterId { get; set; }
    public string Name { get; set; }
    public CoursePageType PageType { get; set; }
}
