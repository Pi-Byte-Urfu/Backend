﻿using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.CoursePages.Dal;

public class TaskPageRepo : BaseRepo<TaskPageModel>, ITaskPageRepo
{
    public TaskPageRepo(AppDatabase database) : base(database)
    {

    }

    public async Task UpdateContent(int pageId, string content)
    {
        var taskPage = await table.Where(taPage => taPage.PageId == pageId).FirstAsync();
        taskPage.Content = content;

        await _database.SaveChangesAsync();
    }
}