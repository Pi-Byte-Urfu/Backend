using Backend.Auth.Dto;
using Backend.Progress.Dto;

namespace Backend.Progress.Logic;

public class ProgressService
{
    public ProgressService()
    {

    }

    public async Task UploadStudentAnswerAsync(UserAuthInfo authInfo, int pageId, ProgressUploadAnswerDto progressUploadAnswerDto)
    {
        throw new NotImplementedException();
    }

    public async Task RateStudentAnswerAsync(int pageId, int studentUserId, ProgressRateAnswerDto rateAnswerDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ProgressGetStudentProgressForCourseDto> GetStudentProgressForCourseAsync(int studentUserId, int courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProgressGetTaskAnswersForStudentDto> GetStudentTasksAnswersAsync(int courseId, int studentUserId)
    {
        throw new NotImplementedException();
    }
}
