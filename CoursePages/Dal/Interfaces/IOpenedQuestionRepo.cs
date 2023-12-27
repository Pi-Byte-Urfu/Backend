// Интерфейс для работы с моделью OpenedQuestionModel
using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces
{
    public interface IOpenedQuestionRepo : IBaseRepo<OpenedQuestionModel>
    {
        // Дополнительные методы, если необходимо
    }
}