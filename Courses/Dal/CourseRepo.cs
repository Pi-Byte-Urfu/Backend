using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class CourseRepo : BaseRepo<CourseModel>, ICourseRepo
{
    public CourseRepo(AppDatabase database) : base(database)
    {

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
