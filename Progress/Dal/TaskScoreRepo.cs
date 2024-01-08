using Backend.Base.Dal;
using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Progress.Dal.Interfaces;
using Backend.Progress.Dal.Models;

namespace Backend.Progress.Dal;

public class TaskScoreRepo : BaseRepo<TaskScoreModel>, ITaskScoreRepo
{
    public TaskScoreRepo(AppDatabase database) : base(database)
    {

    }
}
