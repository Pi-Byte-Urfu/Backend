using Backend.Auth.Dto;
using Backend.Base.Services.Interfaces;
using Backend.CoursePages.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Logic;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Logic;
using Backend.Progress.Dal.Interfaces;
using Backend.Progress.Dal.Models;
using Backend.Progress.Dto;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Backend.Progress.Logic;

public class ProgressService(
    IFileManager fileManager,
    Md5EditorFilesService md5EditorFilesService,
    GroupService groupService,
    ITaskAnswerRepo taskAnswerRepo,
    ITaskScoreRepo taskScoreRepo,
    ICourseChaptersRepo courseChaptersRepo,
    ITaskPageRepo taskPageRepo,
    ICoursePageRepo coursePageRepo
    )
{
    private IFileManager _fileManager = fileManager;
    private Md5EditorFilesService _md5EditorFilesService = md5EditorFilesService;
    private GroupService _groupService = groupService;
    private ITaskAnswerRepo _taskAnswerRepo = taskAnswerRepo;
    private ITaskScoreRepo _taskScoreRepo = taskScoreRepo;
    private ICourseChaptersRepo _courseChaptersRepo = courseChaptersRepo;
    private ITaskPageRepo _taskPageRepo = taskPageRepo;
    private readonly ICoursePageRepo _coursePageRepo = coursePageRepo;

    public async Task<string> UploadStudentAnswerAsync(UserAuthInfo authInfo, int pageId, ProgressUploadAnswerDto progressUploadAnswerDto)
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
        return url;
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
        var studentId = await _groupService.GetStudentIdByUserIdAsync(studentUserId);

        var allStudentScores = (await _taskScoreRepo.GetAllEntitiesAsync()).Where(x => x.StudentId == studentId).ToList();
        var finalScore = allStudentScores.Sum(x => x.StudentScore);

        var allChaptersOfCourse = (await _courseChaptersRepo.GetAllEntitiesAsync()).Where(x => x.CourseId == courseId).ToList();
        var allPagesOfAllChapters = new List<CoursePageModel>();
        foreach (var chapter in allChaptersOfCourse)
            allPagesOfAllChapters.AddRange((await _coursePageRepo.GetAllEntitiesAsync()).Where(x => x.ChapterId == chapter.Id));

        var maxScoreOfCourse = 0;
        foreach (var page in allPagesOfAllChapters)
        {
            var taskPage = (await _taskPageRepo.GetAllEntitiesAsync()).Where(x => x.PageId == page.Id).FirstOrDefault();
            if (taskPage is not null)
                maxScoreOfCourse += taskPage.MaxScore;
        }

        return new ProgressGetStudentProgressForCourseDto() { Progress = (int)((((decimal)finalScore) / ((decimal)maxScoreOfCourse)) * 100) };
    }

    public async Task<ProgressGetTaskAnswersForStudentDto> GetStudentTasksAnswersAsync(int courseId, int studentUserId)
    {
        var studentId = await _groupService.GetStudentIdByUserIdAsync(studentUserId);
        var allAnswers = (await _taskAnswerRepo.GetAllEntitiesAsync()).Where(x => x.StudentId == studentId).ToList();

        var allDtos = new List<ProgressGetTaskAnswersForStudentDto.OneTaskAnswerDto>();
        foreach (var taskAnswer in allAnswers)
            allDtos.Add(await GetOneAnswerTaskDto(taskAnswer));

        return new ProgressGetTaskAnswersForStudentDto() { Answers = allDtos};
    }

    private async Task<ProgressGetTaskAnswersForStudentDto.OneTaskAnswerDto> GetOneAnswerTaskDto(TaskAnswerModel taskAnswer)
    {
        var chapterInfo = await GetTaskAnswerChapterInfo(taskAnswer);
        var pageInfo = await GetTaskAnswerPageInfo(taskAnswer);

        return new ProgressGetTaskAnswersForStudentDto.OneTaskAnswerDto()
        {
            ChapterId = chapterInfo.ChapterId,
            ChapterName = chapterInfo.ChapterName,

            PageId = pageInfo.PageId,
            PageName = pageInfo.PageName,

            StudentScore = await GetStudentTaskScore(taskAnswer),
            MaxScore = await GetTaskMaxScore(taskAnswer),

            FileUrl = taskAnswer.FileUrl,
        };
    }

    internal class ChapterInfo
    {
        public int ChapterId { get; set; }
        public string ChapterName { get; set; }
    }

    private async Task<ChapterInfo> GetTaskAnswerChapterInfo(TaskAnswerModel taskAnswer)
    {
        var pageId = taskAnswer.PageId;
        var page = await _coursePageRepo.GetEntityByIdAsync(pageId);

        var chapter = await _courseChaptersRepo.GetEntityByIdAsync(page.ChapterId);

        return new ChapterInfo { ChapterId = pageId, ChapterName = chapter.Name };
    }

    internal class PageInfo
    {              
        public int PageId { get; set; }
        public string PageName { get; set; }
    }

    private async Task<PageInfo> GetTaskAnswerPageInfo(TaskAnswerModel taskAnswer)
    {
        var pageId = taskAnswer.PageId;
        var page = await _coursePageRepo.GetEntityByIdAsync(pageId);

        return new PageInfo { PageId = pageId, PageName = page.Name };
    }

    private async Task<int> GetStudentTaskScore(TaskAnswerModel taskAnswer)
    {
        return (await _taskScoreRepo.GetAllEntitiesAsync())
            .Where(x => x.StudentId == taskAnswer.StudentId && x.PageId == taskAnswer.PageId)
            .FirstOrDefault()
            ?.StudentScore ?? 0;
    }

    private async Task<int> GetTaskMaxScore(TaskAnswerModel taskAnswer)
    {
        return (await _taskPageRepo.GetAllEntitiesAsync())
            .Where(x => x.PageId == taskAnswer.PageId)
            .First()
            .MaxScore;
    }
}
