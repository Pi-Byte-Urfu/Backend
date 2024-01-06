using Backend.Base.Dal;
using Backend.Courses.Dal.Models;
using Backend.Courses.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class GroupCoursesRepo : BaseRepo<GroupCoursesModel>, IGroupCoursesRepo
{
    public GroupCoursesRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<List<int>> GetCourseIdsByGroupIdAsync(int groupId)
    {
        return await table.Where(x => x.GroupId == groupId).Select(x => x.CourseId).ToListAsync();
    }
}