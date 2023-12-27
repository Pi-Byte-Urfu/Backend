using Backend.Base.Dal.Models;

namespace Backend.CoursePages.Dal.Models;

public class TheoryPageModel : BaseModel
{
    public int PageId { get; set; }
    public string Content { get; set; }
}
