using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Courses.Dal;

public class GroupRepo : BaseRepo<GroupModel>, IGroupRepo
{
    protected AppDatabase _database;

    public GroupRepo(AppDatabase database) : base(database)
    {
        _database = database;
    }

    public async Task<List<GroupModel>> GetAllTeacherGroupsAsync(int teacherId)
    {
        return await table.Where(group => group.TeacherId == teacherId).ToListAsync();
    }

    public async Task<GroupModel> GetGroupByGuidAsync(string groupGuid)
    {
        return await table.FirstOrDefaultAsync(group => group.AddGuid == groupGuid);
    }
}