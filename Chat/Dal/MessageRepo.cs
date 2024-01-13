using Backend.Base.Dal;
using Backend.Chat.Dal.Interfaces;
using Backend.Chat.Dal.Models;

namespace Backend.Chat.Dal;

public class MessageRepo : BaseRepo<MessageModel>, IMessageRepo
{
    public MessageRepo(AppDatabase database) : base(database)
    {

    }
}
