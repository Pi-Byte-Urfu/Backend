﻿using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.CoursePages.Dal;

public class CoursePageRepo : BaseRepo<CoursePageModel>, ICoursePageRepo
{
    public CoursePageRepo(AppDatabase database) : base(database)
    {

    }

    public async Task<List<CoursePageModel>> GetCoursePagesByChapterIdAsync(int chapterId)
    {
        return await table.Where(coursePage => coursePage.ChapterId == chapterId).ToListAsync();
    }

    public async Task UpdateName(int pageId, string name)
    {
        var page = await GetEntityByIdAsync(pageId);
        page.Name = name;

        await _database.SaveChangesAsync();
    }
}