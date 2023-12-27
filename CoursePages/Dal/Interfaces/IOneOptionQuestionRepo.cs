// Интерфейс для работы с моделью OneOptionQuestionModel
using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces
{
    public interface IOneOptionQuestionRepo : IBaseRepo<OneOptionQuestionModel>
    {
        // Дополнительные методы, если необходимо
    }
}