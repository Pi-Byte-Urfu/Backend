using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces;

public interface ITheoryPageRepo : IBaseRepo<TheoryPageModel>
{
    public Task UpdateContent(int pageId, string content);
    public Task DeleteByPageId(int pageId);
}