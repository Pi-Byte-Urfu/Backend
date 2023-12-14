using Backend.Dal.Base;
using Backend.Dal.Courses.Interfaces;
using Backend.Dal.Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dal.Courses;

public class GroupRepo : BaseRepo<GroupModel>, IGroupRepo
{
    private AppDatabase _database;

    public GroupRepo(AppDatabase database) : base(database)
    {
        _database = database;
    }

    public async Task AddStudentToGroupAsync(int studentId, string guid)
    {
        var group = await GetGroupByGuidAsync(guid);
        group.StudentIds = string.Join(",", group.StudentIds
                                                        .Split(',')
                                                        .Append(studentId.ToString())
                                                        .ToList());

        await _database.SaveChangesAsync();
    }

    public async Task<GroupModel> GetGroupByGuidAsync(string groupGuid)
    {
        return await table.FirstOrDefaultAsync(group => group.AddGuid == groupGuid);
    }
}