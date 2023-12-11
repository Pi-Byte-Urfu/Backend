using Backend.Dal.Models;
using Backend.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class GroupRepo : BaseRepo<GroupModel>, IGroupRepo
{
    public GroupRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<GroupModel> GetGroupByGuidAsync(string groupGuid)
    {
        return await table.FirstOrDefaultAsync(group => group.AddGuid == groupGuid);
    }
}