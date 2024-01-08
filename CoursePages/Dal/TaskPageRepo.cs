using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.CoursePages.Dal;

public class TaskPageRepo : BaseRepo<TaskPageModel>, ITaskPageRepo
{
    public TaskPageRepo(AppDatabase database) : base(database)
    {

    }

    public async Task DeleteByPageId(int pageId)
    {
        table.Remove(table.Where(page => page.PageId == pageId).First());
        await _database.SaveChangesAsync();
    }

    public async Task UpdateContent(int pageId, string content)
    {
        var taskPage = await table.Where(taPage => taPage.PageId == pageId).FirstAsync();
        taskPage.Content = content;

        await _database.SaveChangesAsync();
    }

    public async Task UpdateScore(int pageId, int maxScore)
    {
        var taskPage = await table.Where(taPage => taPage.PageId == pageId).FirstAsync();
        taskPage.MaxScore = maxScore;

        await _database.SaveChangesAsync();
    }
}