using Backend.Base.Dto;

namespace Backend.Chat.Dto;

public class ChatGetAllChatsDto : BaseDto
{
    public class ChatDto
    {
        public int ChatId { get; set; }
        public string UserPhoto {  get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public string LastMessageId { get; set; }
    }

    public List<ChatDto> Chats { get; set; }
}

public class MessageDto : BaseDto
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public int MessageId { get; set; }
    public string MessageText { get; set; }
    public bool IsRead { get; set; }
    public long CreatedAt { get; set; }
}

public class ChatGetAllMessagesDto : BaseDto
{
    public List<MessageDto> Messages { get; set; }
}

public class ChatGetOneMessageDto : BaseDto
{
    public MessageDto Message { get; set; }
}

public class ChatSendMessageDto : BaseDto
{
    public int UserId { get; set; }
    public string Text { get; set; }
}