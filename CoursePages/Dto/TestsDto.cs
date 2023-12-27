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