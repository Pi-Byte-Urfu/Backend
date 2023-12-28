using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.CoursePages.Dal;

public class TestPageRepo : BaseRepo<TestPageModel>, ITestPageRepo
{
    public TestPageRepo(AppDatabase database) : base(database)
    {
        
    }

    public async Task<TestPageModel> GetTestPageModelByPageIdAsync(int pageId)
    {
        return await table.Where(testPage => testPage.PageId == pageId).FirstAsync();
    }
}