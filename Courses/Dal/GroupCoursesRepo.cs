using Backend.Base.Dal;
using Backend.Courses.Dal.Models;
using Backend.Courses.Dal.Interfaces;

namespace Backend.Courses.Dal;

public class GroupCoursesRepo : BaseRepo<GroupCoursesModel>, IGroupCoursesRepo
{
    public GroupCoursesRepo(AppDatabase database) : base(database)
    {

    }
}