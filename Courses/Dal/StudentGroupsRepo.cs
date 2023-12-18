using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal;

public class StudentGroupsRepo : BaseRepo<StudentGroupsModel>, IStudentGroupsRepo
{
    public StudentGroupsRepo(AppDatabase database) : base(database)
    {

    }
}
