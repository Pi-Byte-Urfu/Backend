namespace Backend.CoursePages.Dto;

public class QuestionOptionsGetAllDto
{
    public class OptionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }

    public List<OptionDto> Options { get; set; }
}

public class QuestionOpenedGetOneDto
{
    public int Id { get; set; }
    public string Answer { get; set; }
}

public class TestQuestionDto
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public int Difficulty { get; set; }
    public int QuestionScore { get; set; }
    public int SequenceNumber { get; set; }
    public string Text { get; set; }
    public string QuestionType { get; set; }
}

public class OneOptionQuestionDto : TestQuestionDto
{
    public int OneOptionQuestionId { get; set; }
    public List<QuestionOptionDto> Options { get; set; }
}

public class ManyOptionQuestionDto : TestQuestionDto
{
    public int ManyOptionQuestionId { get; set; }
    public List<QuestionOptionDto> Options { get; set; }
}

public class OpenedQuestionDto : TestQuestionDto
{
    public int OpenedQuestionId { get; set; }
    public string Answer { get; set; }
}

public class QuestionOptionDto
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Content { get; set; }
    public bool IsCorrect { get; set; }
}