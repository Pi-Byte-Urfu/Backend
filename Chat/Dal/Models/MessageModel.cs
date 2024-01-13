using Backend.Base.Dal.Models;

namespace Backend.Chat.Dal.Models;

public class MessageModel : BaseModel
{
    public int ChatId { get; set; }
    public int UserId { get; set; }
    public string MessageText { get; set; }
    public bool IsRead { get; set; }
    public long CreatedAt { get; set; }
}
