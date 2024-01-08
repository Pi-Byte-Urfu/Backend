using Backend.Base.Dal.Models;

namespace Backend.Progress.Dal.Models;

public class TaskAnswerModel : BaseModel
{
    public required int PageId { get; set; }
    public required int StudentId { get; set; }
    public required string FileUrl { get; set; }
}
