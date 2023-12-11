using Backend.Dal.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories;

public class TeacherRepo : BaseRepo<TeacherModel>, ITeacherRepo
{
    public TeacherRepo(AppDatabase database) : base(database)
    {

    }
}
