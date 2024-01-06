using Backend.Base.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal.Interfaces;

public interface ICourseRepo : IBaseRepo<CourseModel>
{
    public Task UpdatePhotoPathAsync(int courseId, string photoPath);
    public Task<List<CourseModel>> GetAllTeacherCoursesByCreatorId(int creatorId);
}