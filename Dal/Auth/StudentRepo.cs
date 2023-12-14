using Backend.Dal.Auth.Interfaces;
using Backend.Dal.Auth.Models;
using Backend.Dal.Base;

namespace Backend.Dal.Auth
{
    public class StudentRepo : BaseRepo<StudentModel>, IStudentRepo
    {
        public StudentRepo(AppDatabase database) : base(database)
        {

        }
    }
}
