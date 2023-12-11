using Backend.Dal.Models;

namespace Backend.DataTransferObjects;

public class GroupCreateDto : Dto
{
    public string GroupName { get; init; }
    public int TeacherId { get; init; }
}

public class GroupConnectDto : Dto
{
    public int StudentId { get; init; }
}

public class GroupUpdateDto : UpdateDto<GroupModel>
{
    public string GroupName { get; set; }

    public override GroupModel UpdateEntity(GroupModel entityToUpdate)
    {
        if (GroupName is not null)
            entityToUpdate.GroupName = entityToUpdate.GroupName;

        return entityToUpdate;
    }
}