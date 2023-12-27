using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces;

public interface ICoursePageRepo : IBaseRepo<CoursePageModel>
{
    public Task<List<CoursePageModel>> GetCoursePagesByChapterIdAsync(int chapterId);
}