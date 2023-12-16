using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal;

public class CourseChaptersRepo : BaseRepo<CourseChaptersModel>, ICourseChaptersRepo
{
    public CourseChaptersRepo(AppDatabase database) : base(database)
    {

    }
}