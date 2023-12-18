using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Base.Dal;

namespace Backend.Auth.Dal;

public class TeacherRepo : BaseRepo<TeacherModel>, ITeacherRepo
{
    public TeacherRepo(AppDatabase database) : base(database)
    {

    }
}
