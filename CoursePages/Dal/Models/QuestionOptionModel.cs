using Backend.Base.Dal.Models;

namespace Backend.CoursePages.Dal.Models;

public class QuestionOptionModel : BaseModel
{
    public int QuestionId { get; set; }
    public string Content { get; set; }
    public bool IsCorrect { get; set; }
}