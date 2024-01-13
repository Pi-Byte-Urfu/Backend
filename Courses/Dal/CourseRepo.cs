using Backend.Base.Dal;
using Backend.CoursePages.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Progress.Dal.Interfaces;
using Backend.Progress.Dal.Models;

using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class CourseRepo(
    AppDatabase database,
    ICourseChaptersRepo courseChaptersRepo,
    ICoursePageRepo coursePageRepo,
    ITheoryPageRepo theoryPageRepo,
    ITaskPageRepo taskPageRepo,
    ITaskAnswerRepo taskAnswerRepo,
    ITaskScoreRepo taskScoreRepo
    ) : BaseRepo<CourseModel>(database), ICourseRepo
{
    public async override Task<int> DeleteEntityByIdAsync(int id)
    {
        var chapters = await courseChaptersRepo.GetAllEntitiesAsync();
        chapters = chapters.Where(x => x.CourseId == id).ToList();

        var allPages = new List<CoursePageModel>();
        foreach (var chapter in chapters)
        {
            var pages = await coursePageRepo.GetCoursePagesByChapterIdAsync(chapter.Id);
            allPages.AddRange(pages);
        }

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

        foreach (var chapter in chapters)
            await courseChaptersRepo.DeleteEntityByIdAsync(chapter.Id);

        var id2 = await base.DeleteEntityByIdAsync(id);
        await _database.SaveChangesAsync();

        return id2;
    }

    public async Task<List<CourseModel>> GetAllTeacherCoursesByCreatorId(int creatorId)
    {
        return await table.Where(course=> course.CreatorId ==  creatorId).ToListAsync();
    }

    public async Task UpdatePhotoPathAsync(int courseId, string photoPath)
    {
        var course = await this.GetEntityByIdAsync(courseId);
        course.CoursePhoto = photoPath;
        
        await _database.SaveChangesAsync();
    }
}
