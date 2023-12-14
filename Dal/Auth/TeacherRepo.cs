using Backend.Dal.Auth.Interfaces;
using Backend.Dal.Auth.Models;
using Backend.Dal.Base;

namespace Backend.Dal.Auth;

public class TeacherRepo : BaseRepo<TeacherModel>, ITeacherRepo
{
    public TeacherRepo(AppDatabase database) : base(database)
    {

    }
}
