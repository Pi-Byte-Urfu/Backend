using Backend.Base.Dto;
using Backend.Courses.Dto;
using Backend.Courses.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Courses.Api;


[ApiController]
[Route("api/v1/groups")]
public class GroupController
{
    private GroupService _groupService;

    public GroupController(GroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
    [Route("connect")]
    public async Task<IResult> ConnectToGroup([FromBody] GroupConnectDto connectToGroupDto)
    {
        await _groupService.ConnectToGroupAsync(connectToGroupDto);
        return Results.Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<GroupGetDto> GetGroupByIdAsync([FromRoute] int id)
    {
        return await _groupService.GetGroupByIdAsync(id);
    }

    [HttpGet]
    public async Task<List<GroupGetDto>> GetAllGroups()
    {
        return await _groupService.GetAllGroupsAsync();
    }

    [HttpPost]
    public async Task<BaseIdDto> CreateGroup([FromBody] GroupCreateDto createGroupDto)
    {
        var id = await _groupService.CreateGroupAsync(createGroupDto);
        return new BaseIdDto { Id = id };
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task DeleteGroupById([FromRoute] int id)
    {
        await _groupService.DeleteGroupByIdAsync(id);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task UpdateGroup([FromRoute] int id, [FromBody] GroupUpdateDto updateGroupDto)
    {
        await _groupService.UpdateGroupAsync(id, updateGroupDto);
    }
}
