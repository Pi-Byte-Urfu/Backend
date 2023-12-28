using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.CoursePages.Dal;

public class TestQuestionRepo : BaseRepo<TestQuestionModel>, ITestQuestionRepo
{
    public TestQuestionRepo(AppDatabase database) : base(database)
    {
        
    }

    public async Task DeleteAllQuestionByTestIdAsync(int testId)
    {
        var allQuestionsOfThisTable = await table.Where(que => que.TestId == testId).ToListAsync();
        foreach (var question in allQuestionsOfThisTable)
            table.Remove(question);

        await _database.SaveChangesAsync();
    }

    public async Task<List<TestQuestionModel>> GetAllQuestionsByTestIdAsync(int testId)
    {
        return await table.Where(que => que.TestId == testId).ToListAsync();
    }
}