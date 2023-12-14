using Backend.Dal.Base.Interfaces;
using Backend.Dal.Courses.Models;

namespace Backend.Dal.Courses.Interfaces;

public interface IGroupRepo : IBaseRepo<GroupModel>
{
    public Task<GroupModel> GetGroupByGuidAsync(string groupGuid);
    public Task AddStudentToGroupAsync(int studentId, string guid);
}