using Backend.Dal.Models;

namespace Backend.DataTransferObjects;

public class GroupGetDto : Dto
{
    public string GroupName { get; set; }
    public int TeacherId { get; set; }
    public int[] StudentIds { get; set; }
}

public class GroupCreateDto : Dto
{
    public string GroupName { get; set; }
    public int TeacherId { get; set; }
}

public class GroupConnectDto : Dto
{
    public int StudentId { get; set; }
    public string Guid { get; set; }
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