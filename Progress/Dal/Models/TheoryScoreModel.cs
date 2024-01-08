using Backend.Base.Dal.Models;

namespace Backend.Progress.Dal.Models;

public class TheoryScoreModel : BaseModel
{
    public required int PageId { get; set; }
    public required int StudentId { get; set; }
    public required bool IsPassed { get; set; } = false;
}
