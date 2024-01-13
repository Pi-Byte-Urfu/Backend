using Backend.Base.Dal;
using Backend.Base.Dal.Interfaces;
using Backend.Chat.Dal.Interfaces;
using Backend.Chat.Dal.Models;

namespace Backend.Chat.Dal;

public class ChatRepo : BaseRepo<ChatModel>, IChatRepo
{
    public ChatRepo(AppDatabase database) : base(database)
    {

    }
}
