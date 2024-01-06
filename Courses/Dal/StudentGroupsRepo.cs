using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;

using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class StudentGroupsRepo : BaseRepo<StudentGroupsModel>, IStudentGroupsRepo
{
    public StudentGroupsRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<StudentGroupsModel?> GetStudentGroupByUserAndGroupIdAsync(int studentId, int groupId)
    {
        return await table.Where(connection => connection.StudentId == studentId && connection.GroupId == groupId).FirstOrDefaultAsync();
    }
}
