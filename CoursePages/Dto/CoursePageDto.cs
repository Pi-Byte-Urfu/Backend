using Backend.Base.Dto;
using Backend.CoursePages.Enums;

namespace Backend.CoursePages.Dto;

public class CoursePageCreateDto : BaseDto
{
    public int ChapterId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public CoursePageType PageType { get; set; }
}

public class CoursePageGetAllDto
{
    public class CoursePageBaseGetOneDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CoursePageType PageType { get; set; }
    }

    public List<CoursePageBaseGetOneDto> CoursePages { get; set; }
}

public class CourseTheoryPageGetOneDto
{
    public string Name { get; set; }
    public string Content { get; set; }
}

public class CourseTaskPageGetOneDto
{
    public string Name { get; set; }
    public string Content { get; set; }
}

public class CourseTestPageGetOneDto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int Difficulty { get; set; }
        public int QuestionScore { get; set; }
        public int SequenceNumber { get; set; }
        public string Name { get; set; }
        public TestQuestionType QuestionType { get; set; }
    }
    public string Name { get; set; }
    public List<QuestionDto> Questions { get; set; }
}