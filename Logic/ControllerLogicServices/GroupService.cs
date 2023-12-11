using Backend.Dal.Models;
using Backend.DataTransferObjects;
using Backend.Repositories.Interfaces;

namespace Backend.Logic.ControllerLogicServices;

public class GroupService
{
    private IGroupRepo _groupRepo;

    public GroupService(IGroupRepo groupRepo)
    {
        _groupRepo = groupRepo;
    }

    public GroupModel GetGroupById(int id)
    {
        throw new NotImplementedException();
    }

    public GroupModel GetAllGroups()
    {
        throw new NotImplementedException();
    }

    public GroupModel CreateGroup(GroupCreateDto createGroupDto)
    {
        throw new NotImplementedException();
    }

    public GroupModel DeleteGroupById(int id)
    {
        throw new NotImplementedException();
    }

    public GroupModel UpdateGroup(GroupUpdateDto updateGroupDto)
    {
        throw new NotImplementedException();
    }
}
