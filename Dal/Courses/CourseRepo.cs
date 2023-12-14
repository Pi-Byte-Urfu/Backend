using Backend.Dal.Base;
using Backend.Dal.Courses.Interfaces;
using Backend.Dal.Courses.Models;

namespace Backend.Dal.Courses;

public class CourseRepo : BaseRepo<CourseModel>, ICourseRepo
{
    public CourseRepo(AppDatabase database) : base(database)
    {

    }
}
