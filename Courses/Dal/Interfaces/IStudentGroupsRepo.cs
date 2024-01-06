using Backend.Base.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal.Interfaces;

public interface IStudentGroupsRepo : IBaseRepo<StudentGroupsModel>
{
    public Task<StudentGroupsModel?> GetStudentGroupByUserAndGroupIdAsync(int studentId, int groupId);
    public Task<List<int>> GetAllStudentIdsByGroupIdAsync(int groupId);
}