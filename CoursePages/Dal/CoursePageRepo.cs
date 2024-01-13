using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Backend.Progress.Dal.Models;
using Backend.Progress.Dal;
using Backend.Progress.Dal.Interfaces;

namespace Backend.CoursePages.Dal;

public class CoursePageRepo(
    AppDatabase database,
    ITheoryPageRepo theoryPageRepo,
    ITaskPageRepo taskPageRepo,
    ITaskAnswerRepo taskAnswerRepo,
    ITaskScoreRepo taskScoreRepo
    ) : BaseRepo<CoursePageModel>(database), ICoursePageRepo
{
    public override async Task<int> DeleteEntityByIdAsync(int id)
    {
        var page = await base.GetEntityByIdAsync(id);

        if (page.PageType == Enums.CoursePageType.Theory)
        {
            var theories = new List<TheoryPageModel>();

            var theoryPage = (await theoryPageRepo.GetAllEntitiesAsync()).Where(x => x.PageId == id).First();
            theories.Add(theoryPage);

            foreach (var theory in theories)
                await theoryPageRepo.DeleteEntityByIdAsync(theory.Id);

        }
        else if (page.PageType == Enums.CoursePageType.Task)
        {
            var tasks = new List<TaskPageModel>();
            var taskPage = (await taskPageRepo.GetAllEntitiesAsync()).Where(x => x.PageId == id).First();
            tasks.Add(taskPage);

            var taskAnswers = new List<TaskAnswerModel>();

            var pageId = taskPage.PageId;
            var s = (await taskAnswerRepo.GetAllEntitiesAsync()).Where(x => x.PageId == pageId).ToList();
            taskAnswers.AddRange(s);

            var taskScores = new List<TaskScoreModel>();

            var s2 = (await taskScoreRepo.GetAllEntitiesAsync()).Where(x => x.PageId == pageId).ToList();

            taskScores.AddRange(s2);

            foreach (var score in taskScores)
                await taskScoreRepo.DeleteEntityByIdAsync(score.Id);

            foreach (var answer in taskAnswers)
                await taskAnswerRepo.DeleteEntityByIdAsync(answer.Id);

            foreach (var task in tasks)
                await taskPageRepo.DeleteEntityByIdAsync(task.Id);
        }

        var id2 = await base.DeleteEntityByIdAsync(id);
        await _database.SaveChangesAsync();

        return id2;
    }

    public async Task<List<CoursePageModel>> GetCoursePagesByChapterIdAsync(int chapterId)
    {
        return await table.Where(coursePage => coursePage.ChapterId == chapterId).ToListAsync();
    }

    public async Task UpdateName(int pageId, string name)
    {
        var page = await GetEntityByIdAsync(pageId);
        page.Name = name;

        await _database.SaveChangesAsync();
    }
}