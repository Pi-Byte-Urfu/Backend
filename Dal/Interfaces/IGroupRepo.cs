using Backend.Dal.Models;

namespace Backend.Repositories.Interfaces;

public interface IGroupRepo : IBaseRepo<GroupModel>
{
    public Task<GroupModel> GetGroupByGuidAsync(string groupGuid);
    public Task AddStudentToGroupAsync(int studentId, string guid);
}