using Backend.Dal.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories
{
    public class StudentRepo : BaseRepo<StudentModel>, IStudentRepo
    {
        public StudentRepo(AppDatabase database) : base(database)
        {

        }
    }
}
