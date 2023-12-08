namespace Backend.DataTransferObjects;

public class CreateGroupDto : Dto
{
    public string GroupName { get; init; }
    public int TeacherId { get; init; }
}

public class ConnectToGroupDto : Dto
{
    public int StudentId { get; init; }
}
