using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.CoursePages.Dal;

public class TheoryPageRepo : BaseRepo<TheoryPageModel>, ITheoryPageRepo
{
    public TheoryPageRepo(AppDatabase database) : base(database)
    {

    }

    public async Task UpdateContent(int pageId, string content)
    {
        var theoryPage = await table.Where(thPage => thPage.PageId == pageId).FirstAsync();
        theoryPage.Content = content;

        await _database.SaveChangesAsync();
    }
}