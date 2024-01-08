using Backend.Auth.Dto;
using Backend.Progress.Dto;
using Backend.Progress.Logic;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Progress.Api;

[Route(template: "/api/v1/progress")]
[ApiController]
public class ProgressController
{
    private ProgressService _progressService;

    public ProgressController(ProgressService progressService)
    {
        _progressService = progressService;
    }

    [HttpPost]
    [Route("answer/{pageId}")]
    public async Task<IResult> UploadStudentAnswer([FromHeader] UserAuthInfo authInfo, [FromRoute] int pageId, [FromForm] ProgressUploadAnswerDto uploadAnswerDto)
    {
        await _progressService.UploadStudentAnswerAsync(authInfo, pageId, uploadAnswerDto);
        return Results.Ok();
    }

    [HttpPost]
    [Route("rate/{pageId}/{studentUserId}")]
    public async Task<IResult> RateStudentAnswer([FromRoute] int pageId, [FromRoute] int studentUserId, [FromBody] ProgressRateAnswerDto progressRateAnswerDto)
    {
        await _progressService.RateStudentAnswerAsync(pageId, studentUserId, progressRateAnswerDto);
        return Results.Ok();
    }

    [HttpGet]
    [Route("students/{studentUserId}/courses/{courseId}")]
    public async Task<ProgressGetStudentProgressForCourseDto> GetStudentProgressForCourse([FromRoute] int studentUserId, [FromRoute] int courseId)
    {
        var progress = await _progressService.GetStudentProgressForCourseAsync(studentUserId, courseId);
        return progress;
    }

    [HttpGet]
    [Route("courses/{courseId}/students/{studentUserId}/tasks")]
    public async Task<ProgressGetTaskAnswersForStudentDto> GetStudentTaskAnswers([FromRoute] int courseId, [FromRoute] int studentUserId)
    {
        var studentAnswers = await _progressService.GetStudentTasksAnswersAsync(courseId, studentUserId);
        return studentAnswers;
    }
}
