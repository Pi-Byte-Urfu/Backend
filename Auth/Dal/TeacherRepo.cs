using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dal.Models;
using Backend.Base.Dal;

using Microsoft.EntityFrameworkCore;

namespace Backend.Auth.Dal;

public class TeacherRepo : BaseRepo<TeacherModel>, ITeacherRepo
{
    public TeacherRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<TeacherModel?> GetTeacherByUserId(int userId)
    {
        return await table.Where(teacher => teacher.UserId == userId).FirstOrDefaultAsync();
    }
}
