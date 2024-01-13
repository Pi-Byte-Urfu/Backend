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