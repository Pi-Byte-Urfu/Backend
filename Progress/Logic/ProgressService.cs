using Backend.Auth.Dto;
using Backend.Base.Services.Interfaces;
using Backend.CoursePages.Api;
using Backend.CoursePages.Logic;
using Backend.Courses.Logic;
using Backend.Progress.Dal.Interfaces;
using Backend.Progress.Dal.Models;
using Backend.Progress.Dto;

namespace Backend.Progress.Logic;

public class ProgressService
{
    private IFileManager _fileManager;
    private Md5EditorFilesService _md5EditorFilesService;
    private GroupService _groupService;
    private ITaskAnswerRepo _taskAnswerRepo;
    private ITaskScoreRepo _taskScoreRepo;

    public ProgressService(IFileManager fileManager, Md5EditorFilesService md5EditorFilesService, GroupService groupService, ITaskAnswerRepo taskAnswerRepo, ITaskScoreRepo taskScoreRepo)
    {
        _fileManager = fileManager;
        _md5EditorFilesService = md5EditorFilesService;
        _groupService = groupService;
        _taskAnswerRepo = taskAnswerRepo;
        _taskScoreRepo = taskScoreRepo;
    }

    public async Task UploadStudentAnswerAsync(UserAuthInfo authInfo, int pageId, ProgressUploadAnswerDto progressUploadAnswerDto)
    {
        if (authInfo.UserType is not Auth.Enums.UserType.Student)
            throw new BadHttpRequestException(statusCode: 400, message: "Только ученики могут отвечать на задания");

        var uploadDto = await _md5EditorFilesService.UploadFile(new CoursePages.Dto.Md5EditorUploadFileDto() { FormFile = progressUploadAnswerDto.Answer });
        var url = uploadDto.UrlToGet;

        var studentId = await _groupService.GetStudentIdByUserIdAsync(authInfo.Id);
        var studentAnswerObject = new TaskAnswerModel() { FileUrl = url, StudentId = studentId, PageId = pageId };

        var studentAnswers = (await _taskAnswerRepo.GetAllEntitiesAsync()).Where(x => x.StudentId == studentId && x.PageId == pageId).ToList(); // TODO: change later, do it in DAL
        foreach (var studentAnswer in studentAnswers)
            await _taskAnswerRepo.DeleteEntityByIdAsync(studentAnswer.Id);

        var id = await _taskAnswerRepo.CreateEntityAsync(studentAnswerObject);
    }

    public async Task RateStudentAnswerAsync(int pageId, int studentUserId, ProgressRateAnswerDto rateAnswerDto)
    {
        var studentId = await _groupService.GetStudentIdByUserIdAsync(studentUserId);
        var scores = (await _taskScoreRepo.GetAllEntitiesAsync()).Where(x => x.StudentId == studentId && x.PageId == pageId).ToList();

        foreach (var score in scores)
            await _taskScoreRepo.DeleteEntityByIdAsync(score.Id);

        var newTaskScoreObject = new TaskScoreModel() { PageId = pageId, StudentScore =  rateAnswerDto.Score, StudentId = studentId };
        await _taskScoreRepo.CreateEntityAsync(newTaskScoreObject);
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
