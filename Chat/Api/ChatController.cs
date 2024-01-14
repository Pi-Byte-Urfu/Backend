using Backend.Auth.Dto;
using Backend.Chat.Dal.Models;
using Backend.Chat.Dto;
using Backend.Chat.Logic;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Chat.Api;


[ApiController]
[Route(template: "/api/v1/chat")]
public class ChatController(ChatService chatService) 
{
    private readonly ChatService _chatService = chatService;

    [HttpGet]
    public async Task<ChatGetAllChatsDto> GetAllChats([FromHeader] UserAuthInfo authInfo)
    {
        return await _chatService.GetAllChatsAsync(authInfo);
    }

    [HttpGet]
    [Route("{chatId}/message")]
    public async Task<ChatGetAllMessagesDto> GetAllMessages([FromHeader] UserAuthInfo authInfo, [FromRoute] int chatId)
    {
        return await _chatService.GetAllMessagesAsync(authInfo, chatId);
    }

    [HttpGet]
    [Route("message/{messageId}")]
    public async Task<ChatGetOneMessageDto> GetOneMessage([FromHeader] UserAuthInfo authInfo, [FromRoute] int messageId)
    {
        return await _chatService.GetMessageAsync(authInfo, messageId);
    }

    [HttpPost]
    [Route("{chatId}/message")]
    public async Task<MessageModel> SendMessage([FromHeader] UserAuthInfo authInfo, [FromRoute] int chatId, [FromBody] ChatSendMessageDto messageDto)
    {
        var message = await _chatService.SendMessageAsync(authInfo, chatId, messageDto);
        return message;
    }
}
