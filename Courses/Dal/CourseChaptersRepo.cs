using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class CourseChaptersRepo : BaseRepo<CourseChaptersModel>, ICourseChaptersRepo
{
    public CourseChaptersRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<List<CourseChaptersModel>> GetChaptersByCourseIdAsync(int courseId)
    {
        var allChapters = table.Where(chapter => chapter.CourseId == courseId);
        return await allChapters.ToListAsync();
    }
}