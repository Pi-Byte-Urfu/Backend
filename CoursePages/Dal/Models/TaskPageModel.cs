using Backend.Base.Dal.Models;

namespace Backend.CoursePages.Dal.Models;

public class TaskPageModel : BaseModel
{
    public int PageId { get; set; }
    public int MaxScore { get; set; }
    public string Content { get; set; }
}