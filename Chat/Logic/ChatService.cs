using Backend.Auth.Dal.Interfaces;
using Backend.Auth.Dto;
using Backend.Base.Services;
using Backend.Base.Services.Interfaces;
using Backend.Chat.Dal.Interfaces;
using Backend.Chat.Dal.Models;
using Backend.Chat.Dto;
using Backend.CoursePages.Logic;

namespace Backend.Chat.Logic;

public class ChatService(
    IChatRepo chatRepo,
    IMessageRepo messageRepo,
    IAccountRepo accountRepo,
    Md5EditorFilesService md5EditorFilesService
    )
{
    public async Task<ChatGetAllChatsDto> GetAllChatsAsync(UserAuthInfo authInfo)
    {
        var allChats = await chatRepo.GetAllEntitiesAsync();
        var userChatsWhereGettingUserIsFirst = allChats.Where(x => x.User1Id == authInfo.Id).ToList();
        var userChatsWhereGettingUserIsSecond = allChats.Where(x => x.User2Id == authInfo.Id).ToList();

        var userChats = new List<ChatGetAllChatsDto.ChatDto>();
        foreach (var chat in userChatsWhereGettingUserIsFirst)
            userChats.Add(await GetAllDataToUserChatAndMapToChatDto(authInfo, chat, GettingUserChatIndex.FIRST));
        foreach (var chat in userChatsWhereGettingUserIsSecond)
            userChats.Add(await GetAllDataToUserChatAndMapToChatDto(authInfo, chat, GettingUserChatIndex.SECOND));

        return new ChatGetAllChatsDto() { Chats =  userChats };
    }

    public async Task<ChatGetAllMessagesDto> GetAllMessagesAsync(UserAuthInfo authInfo, int chatId)
    {
        var chat = await chatRepo.GetEntityByIdAsync(chatId);

        var messages = await messageRepo.GetAllEntitiesAsync();
        messages = messages.Where(x => x.ChatId == chatId).ToList();
        var messagesDto = messages.Select(x => MapMessageToDto(authInfo, chat, x)).ToList();
        
        return new ChatGetAllMessagesDto() { Messages = messagesDto };
    }

    private MessageDto MapMessageToDto(UserAuthInfo authInfo, ChatModel chat, MessageModel message)
    {
        return new MessageDto()
        {
            SenderId = message.UserId,
            ReceiverId = chat.User1Id == message.UserId ? chat.User2Id : chat.User1Id,
            MessageId = message.Id,
            MessageText = message.MessageText,
            IsRead = message.IsRead,
            CreatedAt = message.CreatedAt
        };
    }

    public async Task<ChatGetOneMessageDto> GetMessageAsync(UserAuthInfo authInfo, int messageId)
    {
        var message = await messageRepo.GetEntityByIdAsync(messageId);

        var mappedMessageId = (await GetAllMessagesAsync(authInfo, message.ChatId)).Messages.Where(x => x.MessageId == messageId).FirstOrDefault();
        if (mappedMessageId == null)
            throw new BadHttpRequestException(statusCode: 400, message: "Вы пытаетесь получить несуществующее сообщение");

        return new ChatGetOneMessageDto() { Message = mappedMessageId };
    }

    public async Task SendMessageAsync(UserAuthInfo authInfo, int chatId, ChatSendMessageDto chatSendMessageDto)
    {
        var newMessage = new MessageModel() { 
            ChatId = chatId,
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            IsRead = false,
            MessageText = chatSendMessageDto.Text,
            UserId = chatSendMessageDto.UserId
        };

        await messageRepo.CreateEntityAsync(newMessage);
    }

    internal enum GettingUserChatIndex
    {
        FIRST = 1,
        SECOND = 2,
    }

    private async Task<ChatGetAllChatsDto.ChatDto> GetAllDataToUserChatAndMapToChatDto(UserAuthInfo authInfo, ChatModel chatModel, GettingUserChatIndex userChatIndex)
    {
        var otherChatUserId = userChatIndex is GettingUserChatIndex.FIRST ? chatModel.User2Id : chatModel.User1Id;
        var otherChatUserAccountObject = await accountRepo.GetAccountByUserIdAsync(otherChatUserId);
        var lastMessage = (await GetAllMessagesAsync(authInfo, chatModel.Id)).Messages.OrderBy(m => m.CreatedAt).LastOrDefault();

        var newDtoObject = new ChatGetAllChatsDto.ChatDto()
        {
            ChatId = chatModel.Id,
            UserPhoto = md5EditorFilesService.GetApiUrlToPhoto(otherChatUserAccountObject.PhotoUrl),
            UserName = otherChatUserAccountObject.Name,
            UserSurname = otherChatUserAccountObject.Surname,
            UserPatronymic = otherChatUserAccountObject.Patronymic,
            LastMessageId = lastMessage is not null ? lastMessage.MessageId : -1
        };

        return newDtoObject;
    }
}
