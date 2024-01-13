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
    ITaskScoreRepo taskScoreRepo,
    IGroupCoursesRepo groupCoursesRepo
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

        foreach (var chapter in chapters)
            await courseChaptersRepo.DeleteEntityByIdAsync(chapter.Id);

        var allGroupCourses = (await groupCoursesRepo.GetAllEntitiesAsync()).Where(x => x.CourseId == id).ToList();
        foreach (var groupCourse in allGroupCourses)
            await groupCoursesRepo.DeleteEntityByIdAsync(groupCourse.Id);

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
