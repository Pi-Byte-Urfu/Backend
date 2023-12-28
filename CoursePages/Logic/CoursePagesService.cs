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

    public async Task UpdateTheoryPage(int pageId, CourseTheoryPageUpdateDto courseTheoryPageUpdateDto)
    {
        if (courseTheoryPageUpdateDto.Name is not null)
            await _coursePageRepo.UpdateName(pageId, courseTheoryPageUpdateDto.Name);
        if (courseTheoryPageUpdateDto.Content is not null)
            await _theoryPageRepo.UpdateContent(pageId, courseTheoryPageUpdateDto.Content);
    }

    public async Task UpdateTaskPage(int pageId, CourseTaskPageUpdateDto courseTaskPageUpdate)
    {
        if (courseTaskPageUpdate.Name is not null)
            await _coursePageRepo.UpdateName(pageId, courseTaskPageUpdate.Name);
        if (courseTaskPageUpdate.Content is not null)
            await _taskPageRepo.UpdateContent(pageId, courseTaskPageUpdate.Content);
    }

    public async Task UpdateTestPage(int pageId, CourseTestPageUpdateDto courseTestPageUpdate)
    {
        if (courseTestPageUpdate.Name is not null)
            await _coursePageRepo.UpdateName(pageId, courseTestPageUpdate.Name);
    }

    public async Task DeletePage(int pageId)
    {
        var basePage = await _coursePageRepo.GetEntityByIdAsync(pageId);
        switch (basePage.PageType)
        {
            case Enums.CoursePageType.Theory:
                await DeleteTheoryPage(pageId);
                break;
            case Enums.CoursePageType.Test:
                await DeleteTestPage(pageId);
                break;
            case Enums.CoursePageType.Task:
                await DeleteTaskPage(pageId);
                break;
            default:
                break;
        }

        await _coursePageRepo.DeleteEntityByIdAsync(pageId);
    }

    private async Task DeleteTheoryPage(int pageId)
    {
        await _theoryPageRepo.DeleteByPageId(pageId);
    }

    private async Task DeleteTaskPage(int pageId)
    {
        await _taskPageRepo.DeleteByPageId(pageId);
    }

    private async Task DeleteTestPage(int pageId)
    {
        var testPage = await _testPageRepo.GetTestPageModelByPageIdAsync(pageId);
        // await DeleteAllTypesOfQuestions(testPage.Id); TODO: LATER

        await _testQuestionRepo.DeleteAllQuestionByTestIdAsync(pageId);
        await _testPageRepo.DeleteEntityByIdAsync(pageId);
    }
    private async Task DeleteAllTypesOfQuestions(int testId)
    {
        throw new NotImplementedException();
    }

    private CoursePageGetAllDto MapAllPagesToGetAllDto(List<CoursePageModel> coursePageModels)
    {
        return new CoursePageGetAllDto()
        {
            CoursePages = coursePageModels
                .Select(x => new CoursePageGetAllDto.CoursePageBaseGetOneDto() { Id = x.Id, Name = x.Name, PageType = x.PageType })
                .OrderBy(x => x.Id)
                .ToList()
        };
    }

    private async Task CreateSubPage(int pageId, CoursePageModel pageModel)
    {
        if (pageModel.PageType is Enums.CoursePageType.Theory)
        {
            await _theoryPageRepo.CreateEntityAsync(new TheoryPageModel()
            {
                Content = "",
                PageId = pageId,
            });
        }
        else if (pageModel.PageType is Enums.CoursePageType.Test)
        {
            await _testPageRepo.CreateEntityAsync(new TestPageModel()
            {
                PageId = pageId,
            });
        }
        else if (pageModel.PageType is Enums.CoursePageType.Task)
        {
            await _taskPageRepo.CreateEntityAsync(new TaskPageModel()
            {
                Content = "",
                PageId = pageId,
            });
        }
        else
            throw new Exception("Don't know this type");
    }

    private CourseTheoryPageGetOneDto MapTheoryPageToDto(CoursePageModel baseModel, TheoryPageModel theoryPageModel)
    {
        return new CourseTheoryPageGetOneDto() { Content = theoryPageModel.Content, Name = baseModel.Name };
    }

    private CourseTestPageGetOneDto MapTestPageToDto(CoursePageModel baseModel, List<TestQuestionModel> questionModels)
    {
        return new CourseTestPageGetOneDto()
        {
            Name = baseModel.Name,
            Questions = questionModels.Select(que => new CourseTestPageGetOneDto.QuestionDto()
            {
                Id = que.Id,
                Difficulty = que.Difficulty,
                QuestionScore = que.QuestionScore,
                SequenceNumber = que.SequenceNumber,
                Text = que.Text,
                QuestionType = que.QuestionType,
            })
            .ToList()
        };
    }

    private CourseTaskPageGetOneDto MapTaskPageToDto(CoursePageModel baseModel, TaskPageModel taskPageModel)
    {
        return new CourseTaskPageGetOneDto() { Content = taskPageModel.Content, Name = baseModel.Name };
    }
}
