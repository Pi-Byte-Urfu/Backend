using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces;

public interface ITestQuestionRepo : IBaseRepo<TestQuestionModel>
{
    public Task<List<TestQuestionModel>> GetAllQuestionsByTestIdAsync(int testId);
    public Task DeleteAllQuestionByTestIdAsync(int testId);
}