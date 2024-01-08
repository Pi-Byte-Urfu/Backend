using Backend.Base.Dto;

namespace Backend.Progress.Dto;

public class ProgressUploadAnswerDto : BaseDto
{
    public IFormFile Answer { get; set; }
}

public class ProgressRateAnswerDto : BaseDto
{
    public int Score { get; set; }
}

public class ProgressGetStudentProgressForCourseDto : BaseDto
{
    public int Progress { get; set; }
}

public class ProgressGetTaskAnswersForStudentDto : BaseDto
{
    public class OneTaskAnswerDto : BaseDto
    {
        public int ChapterId {  get; set; }
        public int ChapterName { get; set; }

        public int PageId { get; set; }
        public int PageName { get; set; }

        public int StudentScore { get; set; }
        public int MaxScore { get; set; }

        public string FileUrl { get; set; }
    }

    public List<OneTaskAnswerDto> Answers { get; set; }
}