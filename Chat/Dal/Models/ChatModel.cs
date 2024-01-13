using Backend.Base.Dal.Models;

namespace Backend.Chat.Dal.Models;

public class ChatModel : BaseModel
{
    public int User1Id { get; set; }
    public int User2Id { get; set; }
}
