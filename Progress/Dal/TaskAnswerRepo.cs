using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Progress.Dal.Interfaces;
using Backend.Progress.Dal.Models;

namespace Backend.Progress.Dal;

public class TaskAnswerRepo : BaseRepo<TaskAnswerModel>, ITaskAnswerRepo
{
    public TaskAnswerRepo(AppDatabase database) : base(database)
    {
    }
}
