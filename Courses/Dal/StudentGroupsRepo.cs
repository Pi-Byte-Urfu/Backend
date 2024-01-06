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

    public async Task<List<int>> GetAllStudentIdsByGroupIdAsync(int groupId)
    {
        return await table.Where(studentConnection => studentConnection.GroupId == groupId).Select(studentConnection => studentConnection.StudentId).ToListAsync();
    }

    public async Task<List<int>> GetGroupIdsByStudentIdAsync(int studentId)
    {
        return await table.Where(conn => conn.StudentId == studentId).Select(conn => conn.GroupId).ToListAsync();
    }

    public async Task<StudentGroupsModel?> GetStudentGroupByUserAndGroupIdAsync(int studentId, int groupId)
    {
        return await table.Where(connection => connection.StudentId == studentId && connection.GroupId == groupId).FirstOrDefaultAsync();
    }
}
