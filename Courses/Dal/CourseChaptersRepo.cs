using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Progress.Dal.Models;
using Backend.Progress.Dal;

using Microsoft.EntityFrameworkCore;
using Backend.CoursePages.Dal.Interfaces;
using Backend.Progress.Dal.Interfaces;

namespace Backend.Courses.Dal;

public class CourseChaptersRepo(
    AppDatabase database,
    ICoursePageRepo coursePageRepo,
    ITheoryPageRepo theoryPageRepo,
    ITaskPageRepo taskPageRepo,
    ITaskAnswerRepo taskAnswerRepo,
    ITaskScoreRepo taskScoreRepo
    ) : BaseRepo<CourseChaptersModel>(database), ICourseChaptersRepo
{
    public async override Task<int> DeleteEntityByIdAsync(int id)
    {
        var allPages = new List<CoursePageModel>();
        var pages = await coursePageRepo.GetCoursePagesByChapterIdAsync(id);
        allPages.AddRange(pages);

        var theories = new List<TheoryPageModel>();
        var tasks = new List<TaskPageModel>();
        foreach (var page in allPages)
        {
            if (page.PageType == CoursePages.Enums.CoursePageType.Theory)
            {
                var theoryPage = (await theoryPageRepo.GetAllEntitiesAsync()).Where(x => x.PageId == page.Id).First();
                theories.Add(theoryPage);
            }
            else if (page.PageType == CoursePages.Enums.CoursePageType.Task)
            {
                var taskPage = (await taskPageRepo.GetAllEntitiesAsync()).Where(x => x.PageId == page.Id).First();
                tasks.Add(taskPage);
            }
        }

        var taskAnswers = new List<TaskAnswerModel>();
        foreach (var taskPage in tasks)
        {
            var pageId = taskPage.PageId;
            var s = (await taskAnswerRepo.GetAllEntitiesAsync()).Where(x => x.PageId == pageId).First();
            taskAnswers.Add(s);
        }

        var taskScores = new List<TaskScoreModel>();
        foreach (var taskPage in tasks)
        {
            var pageId = taskPage.PageId;
            var s = (await taskScoreRepo.GetAllEntitiesAsync()).Where(x => x.PageId == pageId).First();
            taskScores.Add(s);
        }

        foreach (var score in taskScores)
            await taskScoreRepo.DeleteEntityByIdAsync(score.Id);

        foreach (var answer in taskAnswers)
            await taskAnswerRepo.DeleteEntityByIdAsync(answer.Id);

        foreach (var task in tasks)
            await taskPageRepo.DeleteEntityByIdAsync(task.Id);

        foreach (var theory in theories)
            await theoryPageRepo.DeleteEntityByIdAsync(theory.Id);

        foreach (var page in allPages)
            await coursePageRepo.DeleteEntityByIdAsync(page.Id);

        var id2 = await base.DeleteEntityByIdAsync(id);
        await _database.SaveChangesAsync();

        return id2;
    }

    public async Task<List<CourseChaptersModel>> GetChaptersByCourseIdAsync(int courseId)
    {
        var allChapters = table.Where(chapter => chapter.CourseId == courseId);
        return await allChapters.ToListAsync();
    }
}