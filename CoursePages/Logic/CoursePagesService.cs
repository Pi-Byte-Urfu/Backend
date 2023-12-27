using Backend.Base.Dto;
using Backend.CoursePages.Dal;
using Backend.CoursePages.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dto;

namespace Backend.CoursePages.Logic;

public class CoursePagesService
{
    private readonly ICoursePageRepo _coursePageRepo;
    private readonly ITheoryPageRepo _theoryPageRepo;
    private readonly ITaskPageRepo _taskPageRepo;
    private readonly ITestPageRepo _testPageRepo;
    private readonly ITestQuestionRepo _testQuestionRepo;

    public CoursePagesService(
        ICoursePageRepo coursePageRepo,
        ITheoryPageRepo theoryPageRepo,
        ITaskPageRepo taskPageRepo,
        ITestPageRepo testPageRepo,
        ITestQuestionRepo testQuestionRepo)
    {
        _coursePageRepo = coursePageRepo;
        _theoryPageRepo = theoryPageRepo;
        _taskPageRepo = taskPageRepo;
        _testPageRepo = testPageRepo;
        _testQuestionRepo = testQuestionRepo;
    }

    public async Task<CoursePageGetAllDto> GetAllCoursePagesByChapterId(int chapterId)
    {
        var allCoursesPages = await _coursePageRepo.GetCoursePagesByChapterIdAsync(chapterId);
        return MapAllPagesToGetAllDto(allCoursesPages);
    }

    public async Task<BaseIdDto> CoursePageCreateDto(CoursePageCreateDto coursePageCreateDto)
    {
        var newCoursePage = new CoursePageModel()
        {
            ChapterId = coursePageCreateDto.ChapterId,
            Name = coursePageCreateDto.Name is not null ? coursePageCreateDto.Name : string.Empty,
            PageType = coursePageCreateDto.PageType
        };
        var id = await _coursePageRepo.CreateEntityAsync(newCoursePage);
        await CreateSubPage(id, newCoursePage);

        return new BaseIdDto { Id = id };
    }

    public async Task<CourseTheoryPageGetOneDto> GetTheoryPage(int pageId)
    {
        var basePage = await _coursePageRepo.GetEntityByIdAsync(pageId);
        var theoryPage = (await _theoryPageRepo.GetAllEntitiesAsync()).Where(page => page.PageId == pageId).First();

        return MapTheoryPageToDto(basePage, theoryPage);
    }

    public async Task<CourseTaskPageGetOneDto> GetTaskPage(int pageId)
    {
        var basePage = await _coursePageRepo.GetEntityByIdAsync(pageId);
        var taskPage = (await _taskPageRepo.GetAllEntitiesAsync()).Where(page => page.PageId == pageId).First();

        return MapTaskPageToDto(basePage, taskPage);
    }

    public async Task<CourseTestPageGetOneDto> GetTestPage(int pageId)
    {
        var basePage = await _coursePageRepo.GetEntityByIdAsync(pageId);
        var questions = (await _testQuestionRepo.GetAllEntitiesAsync()).Where(que => que.TestId == pageId).ToList();

        return MapTestPageToDto(basePage, questions);
    }

    private CoursePageGetAllDto MapAllPagesToGetAllDto(List<CoursePageModel> coursePageModels)
    {
        throw new NotImplementedException();
    }

    private Task CreateSubPage(int pageId, CoursePageModel pageModel)
    {
        throw new NotImplementedException();
    }

    private CourseTheoryPageGetOneDto MapTheoryPageToDto(CoursePageModel baseModel, TheoryPageModel theoryPageModel)
    {
        throw new NotImplementedException();
    }

    private CourseTestPageGetOneDto MapTestPageToDto(CoursePageModel baseModel, List<TestQuestionModel> questionModels)
    {
        throw new NotImplementedException();
    }

    private CourseTaskPageGetOneDto MapTaskPageToDto(CoursePageModel baseModel, TaskPageModel taskPageModel)
    {
        throw new NotImplementedException();
    }

    //public async Task<QuestionOptionsGetAllDto> GetQuestionOptions(int questionId)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<QuestionOpenedGetOneDto> GetOpenedQuestion(int questionId)
    //{
    //    throw new NotImplementedException();
    //}
}
