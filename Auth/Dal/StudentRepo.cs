using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Base.Dal;

using Microsoft.EntityFrameworkCore;

namespace Backend.Auth.Dal
{
    public class StudentRepo : BaseRepo<StudentModel>, IStudentRepo
    {
        public StudentRepo(AppDatabase database) : base(database)
        {

        }

        public async Task<StudentModel> GetStudentByUserId(int userId)
        {
            return await table.Where(student => student.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<int> GetUserIdByStudentId(int studentId)
        {
            return await table.Where(student => student.Id == studentId).Select(student => student.UserId).FirstOrDefaultAsync();
        }
    }
}
