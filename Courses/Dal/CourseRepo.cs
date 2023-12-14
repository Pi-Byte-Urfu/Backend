using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal;

public class CourseRepo : BaseRepo<CourseModel>, ICourseRepo
{
    public CourseRepo(AppDatabase database) : base(database)
    {

    }
}
