using Backend.Base.Dal.Interfaces;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dal.Interfaces;

public interface IGroupRepo : IBaseRepo<GroupModel>
{
    public Task<GroupModel> GetGroupByGuidAsync(string groupGuid);
}