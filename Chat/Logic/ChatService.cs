using Backend.Auth.Dto;
using Backend.Chat.Dto;

namespace Backend.Chat.Logic;

public class ChatService
{
    public ChatService()
    {

    }

    public async Task<ChatGetAllChatsDto> GetAllChatsAsync(UserAuthInfo authInfo)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatGetAllMessagesDto> GetAllMessagesAsync(UserAuthInfo authInfo, int chatId)
    {
        throw new NotImplementedException();
    }

    public async Task<ChatGetOneMessageDto> GetMessageAsync(int messageId)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessageAsync(UserAuthInfo authInfo, int chatId, ChatSendMessageDto chatSendMessageDto)
    {
        throw new NotImplementedException();
    }
}
