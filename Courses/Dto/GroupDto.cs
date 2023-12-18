using Backend.Base.Dto;
using Backend.Courses.Dal.Models;

namespace Backend.Courses.Dto;

public class GroupGetDto : BaseDto
{
    public string GroupName { get; set; }
    public int TeacherId { get; set; }
}

public class GroupCreateDto : BaseDto
{
    public string GroupName { get; set; }
    public int TeacherId { get; set; }
}

public class GroupConnectDto : BaseDto
{
    public int StudentId { get; set; }
    public string Guid { get; set; }
}

public class GroupUpdateDto : BaseUpdateDto<GroupModel>
{
    public string GroupName { get; set; }

    public override GroupModel UpdateEntity(GroupModel entityToUpdate)
    {
        if (GroupName is not null)
            entityToUpdate.GroupName = entityToUpdate.GroupName;

        return entityToUpdate;
    }
}