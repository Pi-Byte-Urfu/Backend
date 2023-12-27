using Backend.Base.Dal.Models;
using Backend.CoursePages.Enums;

namespace Backend.CoursePages.Dal.Models;

public class TestQuestionModel : BaseModel
{
    public int TestId { get; set; }
    public int Difficulty { get; set; }
    public int QuestionScore {  get; set; }
    public int SequenceNumber { get; set; }
    public string Text { get; set; }
    public TestQuestionType QuestionType { get; set; }
}
