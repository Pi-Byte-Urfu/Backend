// Интерфейс для работы с моделью ManyOptionQuestionModel
using Backend.Base.Dal.Interfaces;
using Backend.CoursePages.Dal.Models;

namespace Backend.CoursePages.Dal.Interfaces;

public interface IManyOptionQuestionRepo : IBaseRepo<ManyOptionQuestionModel>
{
    // Дополнительные методы, если необходимо
}