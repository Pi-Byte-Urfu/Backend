using Backend.Base.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal.Interfaces;

public interface IGroupCoursesRepo : IBaseRepo<GroupCoursesModel>
{
    public Task<List<int>> GetCourseIdsByGroupIdAsync(int groupId);
}