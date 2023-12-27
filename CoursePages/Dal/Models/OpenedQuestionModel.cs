using Backend.Base.Dal.Models;

namespace Backend.CoursePages.Dal.Models;

public class OpenedQuestionModel : BaseModel
{
    public int QuestionId { get; set; }
    public string Answer {  get; set; }
}
