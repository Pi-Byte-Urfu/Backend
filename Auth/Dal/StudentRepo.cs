using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Base.Dal;

namespace Backend.Auth.Dal
{
    public class StudentRepo : BaseRepo<StudentModel>, IStudentRepo
    {
        public StudentRepo(AppDatabase database) : base(database)
        {

        }
    }
}
