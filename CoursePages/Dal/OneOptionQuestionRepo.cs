using Backend.Base.Dal;
using Backend.CoursePages.Dal.Models;
using Backend.CoursePages.Dal.Interfaces;

namespace Backend.CoursePages.Dal
{
    public class OneOptionQuestionRepo : BaseRepo<OneOptionQuestionModel>, IOneOptionQuestionRepo
    {
        public OneOptionQuestionRepo(AppDatabase database) : base(database)
        {
            // Любая специфичная логика для работы с моделью OneOptionQuestionModel
        }
    }
}