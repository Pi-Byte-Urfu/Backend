using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces;

public interface ITestPageRepo : IBaseRepo<TestPageModel>
{
    public Task<TestPageModel> GetTestPageModelByPageIdAsync(int pageId);
}