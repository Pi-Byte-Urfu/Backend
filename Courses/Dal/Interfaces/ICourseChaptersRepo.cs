using Backend.Base.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal.Interfaces;

public interface ICourseChaptersRepo : IBaseRepo<CourseChaptersModel>
{
    public Task<List<CourseChaptersModel>> GetChaptersByCourseIdAsync(int courseId);
}