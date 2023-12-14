using Backend.Courses.Dal.Interfaces;
using Backend.Courses.Dal.Models;
using Backend.Courses.Dto;

namespace Backend.Courses.Logic;

public class GroupService
{
    private IGroupRepo _groupRepo;

    public GroupService(IGroupRepo groupRepo)
    {
        _groupRepo = groupRepo;
    }

    public async Task<GroupGetDto> GetGroupByIdAsync(int id)
    {
        var group = await _groupRepo.GetEntityByIdAsync(id);
        return MapGroupToGetDto(group);
    }

    public async Task<List<GroupGetDto>> GetAllGroupsAsync()
    {
        var groups = await _groupRepo.GetAllEntitiesAsync();
        return groups.Select(MapGroupToGetDto).ToList();
    }

    public async Task<int> CreateGroupAsync(GroupCreateDto createGroupDto)
    {
        var newGroup = new GroupModel()
        {
            GroupName = createGroupDto.GroupName,
            AddGuid = Guid.NewGuid().ToString(),
            StudentIds = "",
            TeacherId = createGroupDto.TeacherId,
        };

        var id = await _groupRepo.CreateEntityAsync(newGroup);
        return id;
    }

    public async Task DeleteGroupByIdAsync(int id)
    {
        await _groupRepo.DeleteEntityByIdAsync(id);
    }

    public async Task UpdateGroupAsync(int id, GroupUpdateDto updateGroupDto)
    {
        await _groupRepo.UpdateEntityAsync(id, updateGroupDto);
    }

    public async Task ConnectToGroupAsync(GroupConnectDto connectToGroupDto)
    {
        // TODO: Add check if studen already in group
        await _groupRepo.AddStudentToGroupAsync(connectToGroupDto.StudentId, connectToGroupDto.Guid);
    }

    // TODO: Make deleting student from gorup
    // TODO: Make regenerating GUID (link)

    private GroupGetDto MapGroupToGetDto(GroupModel group)
    {
        return new GroupGetDto()
        {
            GroupName = group.GroupName,
            TeacherId = group.TeacherId,
            StudentIds = group.StudentIds.Split(',').Select(id => int.Parse(id)).ToArray(),
        };
    }
}
