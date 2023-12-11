﻿using Backend.Dal.Models;
using Backend.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

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